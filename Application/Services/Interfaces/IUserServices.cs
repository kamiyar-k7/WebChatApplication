using Application.ViewModel_And_Dto.Dto.UserSide;

namespace Application.Services.Interfaces;

public interface IUserServices
{
    #region Auth

    Task SignUp(UserSignUpDto userDto);

    Task<string> SignIn(UserSignInDto userDto);
    #endregion


    Task<List<OtherUserDto>> FindUsers(string UserName);
}
