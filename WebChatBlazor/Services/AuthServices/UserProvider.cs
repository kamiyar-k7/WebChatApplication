using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace WebChatBlazor.Services.AuthServices;

public class UserProvider
{
    private readonly AuthStateProvider _stateProvider;
    public UserProvider(AuthStateProvider authStateProvider)
    {
            _stateProvider = authStateProvider; 
    }

    public async Task<UserContext> SetCurrentUserFromClaims()
    {
        var claims = await  _stateProvider.GetClaims();

        if (claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) != null)
        {
            // Extract user details from claims
            var userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userNameClaim = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value;


            UserContext userContext = new UserContext()
            {
                UserId = int.Parse(userIdClaim),
                UserName = userNameClaim
            };
            return userContext;
          
        }
        else
        {
            return new UserContext();
        }
    }
}
public class UserContext
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    // You can add more user-related properties if needed.
}
