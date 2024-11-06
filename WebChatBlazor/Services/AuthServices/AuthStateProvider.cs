using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;


public partial class AuthStateProvider : AuthenticationStateProvider
{

    #region Ctor
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _TokenHandler;

    public AuthStateProvider(ILocalStorageService localStorage, JwtSecurityTokenHandler jwtSecurityTokenHandler)
    {
        this._localStorage = localStorage;
        this._TokenHandler = jwtSecurityTokenHandler;
    }

    #endregion

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var notloggedin = new ClaimsPrincipal(new ClaimsIdentity());

        var savedtoken = await _localStorage.GetItemAsync<string>("token");
        if (savedtoken == null)
        {
            return new AuthenticationState(notloggedin);
        }


        var tokencontent = _TokenHandler.ReadJwtToken(savedtoken);
        if (tokencontent.ValidTo < DateTime.UtcNow)
        {
            return new AuthenticationState(notloggedin);
        }

        var claims = await GetClaims();

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        return new AuthenticationState(user);

    }

    public async Task LoggedIn()
    {

        var claims = await GetClaims();
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        var authstate = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authstate);
    }

    public void LoggedOut()
    {
        var notloggedin = new ClaimsPrincipal(new ClaimsIdentity());
        var authstate = Task.FromResult(new AuthenticationState(notloggedin));
        NotifyAuthenticationStateChanged(authstate);
    }
    public async Task<List<Claim>> GetClaims()
    {
        var savedtoken = await _localStorage.GetItemAsync<string>("token");
        var tokencontent = _TokenHandler.ReadJwtToken(savedtoken);
        var claims = tokencontent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokencontent.Subject));
        return claims;
    }

    

    // fixed attribute
    public class BlazorAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            return next(context);
        }
    }
}