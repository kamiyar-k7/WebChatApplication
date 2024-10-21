using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.AuthServices;

public class AuthenticationService : IAuthenticationService
{
    private readonly AuthenticationStateProvider _authprovider;
    private readonly IClient _client;
    private readonly ILocalStorageService _localStorage;

    #region Ctor
    public AuthenticationService(AuthenticationStateProvider authenticationState ,IClient client, ILocalStorageService localStorage)
    {
        _authprovider = authenticationState;
        _client = client;
        _localStorage = localStorage;
    }
    #endregion

    public async Task SignUp(UserSignUpDto userdto)
    {
        await _client.SignUpAsync(userdto);

    }
    public async Task SignIn(UserSignInDto userdto)
    {
        var token = await _client.SignInAsync(userdto);

        //store Token
        await _localStorage.SetItemAsync("token", token);

        //change auth of app
         await   ((AuthStateProvider)_authprovider).LoggedIn();

    }

    public async Task GetToken()
    {
        var token = await _localStorage.GetItemAsync<string>("token");
        if (token != null)
        {
            // Set the Authorization header with the bearer token
            _client.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

}
