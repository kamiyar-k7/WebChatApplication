using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.UserServices;

public interface IUserServices
{
    Task SignUp(UserSignUpDto userdto);
    Task SignIn(UserSignInDto userdto);
}
