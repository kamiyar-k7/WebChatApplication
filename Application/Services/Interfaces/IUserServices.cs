using Application.ViewModel_And_Dto.Dto.UserSide;

namespace Application.Services.Interfaces;

public interface IUserServices
{
    #region Auth
    Task GetEmailForSignUp(UserSignUpDto userSignUpDto);
    Task<bool> VerifyCode(UserVerifyDto verifyDto);
    Task ResenVerifyCode(string UserEmial);


    Task<string> SignIn(UserSignInDto userDto);
    #endregion


    Task<List<OtherUserDto>> FindUsers(string UserName);
    Task<OtherUserDto> GetOtheUserDetails(int id);
}
