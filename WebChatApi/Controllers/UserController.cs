using Application.Services.Interfaces;
using Application.ViewModel_And_Dto.Dto.UserSide;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebChatApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    #region Ctor
    private readonly IUserServices _userServices;
    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }
    #endregion



    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("[Action]")]
    public async Task<IActionResult> AddNewUser(UserDto userDto)
    {

        try
        {
            await _userServices.AddUser(userDto);
            return CreatedAtAction(nameof(AddNewUser) , new {userDto} );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
            
        }

    }



}
