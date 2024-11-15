using Application.Services.Interfaces;
using Application.ViewModel_And_Dto.Dto.UserSide;
using Microsoft.AspNetCore.Mvc;

namespace WebChatApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    #region Ctor
    private readonly IUserServices _userServices;
    public AuthController(IUserServices userServices)
    {
        _userServices = userServices;
    }
    #endregion

    #region SignUp

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("[Action]")]
    public async Task<IActionResult> GetEmailForSignUp(UserSignUpDto userDto)
    {

        try
        {
            await _userServices.GetEmailForSignUp(userDto);
            return Ok(userDto.UserEmail);
        }
        catch (Exception ex)
        {
           
            return BadRequest(ex.Message);

        }

    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("[Action]")]
    public async Task<ActionResult<bool>> VerifyAndSignUser(UserVerifyDto verifyDto)
    {
        try
        {
            var res = await _userServices.VerifyCode(verifyDto);
            return Ok(res);
        }
        catch (Exception ex)
        {

            return Unauthorized(ex.Message);
        }


    }

    #endregion

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("[Action]")]
    public async Task<ActionResult<string>> SignIn(UserSignInDto userSignInDto)
    {
        try
        {

            var token = await _userServices.SignIn(userSignInDto);
            if (token == null)
            {
                return Unauthorized("The Email or Password Are Incorecct!");
            }
            return Ok(token);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }

    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("[Action]")]
    public async Task<IActionResult> ResendVerifyCode(string UserEmail)
    {
        try
        {
            await _userServices.ResenVerifyCode(UserEmail);
            return Ok();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }


}
