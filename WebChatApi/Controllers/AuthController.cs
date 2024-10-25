using Application.Services.Interfaces;
using Application.ViewModel_And_Dto.Dto.UserSide;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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



    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("[Action]")]
    public async Task<IActionResult> SignUp(UserSignUpDto userDto)
    {

        try
        {
            await _userServices.SignUp(userDto);
            return CreatedAtAction(nameof(SignUp), new { userDto.UserName });
        }
        catch (Exception ex)
        {
           
            return BadRequest(ex.Message);

        }

    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("[Action]")]
    public async Task<ActionResult<string>> SignIn(UserSignInDto userSignInDto)
    {
        try
        {

            var token = await _userServices.SignIn(userSignInDto);
            if (token == null)
            {
                return Unauthorized("The Error or Password Are Incorecct!");
            }
            return Ok(token);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }


    }

}
