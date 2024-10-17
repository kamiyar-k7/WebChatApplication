using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.UserServices;

public class UserServices : IUserServices
{
    private readonly IClient _client;

    #region Ctor
    public UserServices( IClient client)
    {
        _client = client;
    }
    #endregion

    public async Task SignUp(UserSignUpDto userdto)
    { 
            await _client.SignUpAsync(userdto);

    } 
    public async Task SignIn(UserSignInDto userdto)
    { 
        var token =    await _client.SignInAsync(userdto);
        token = string.Empty;
    }

}
