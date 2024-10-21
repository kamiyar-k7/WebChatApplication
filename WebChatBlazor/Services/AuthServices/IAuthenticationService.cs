using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.AuthServices;

public interface IAuthenticationService
{
    Task SignUp(UserSignUpDto userdto);
    Task SignIn(UserSignInDto userdto);
    Task GetToken();
}
