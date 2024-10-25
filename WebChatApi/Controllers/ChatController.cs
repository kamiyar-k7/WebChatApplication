using Application.Services.Interfaces;
using Application.ViewModel_And_Dto.Dto.UserSide;
using Microsoft.AspNetCore.Mvc;


namespace WebChatApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ChatController : ControllerBase
{


    #region Ctor
    private readonly IChatServices _chatservice;
    private readonly IUserServices _userservice;
    public ChatController(IChatServices chatservice, IUserServices userServices)
    {
        _chatservice = chatservice;
        _userservice = userServices;
    }

    #endregion

    [HttpPost("[Action]/{UserName}")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    public async Task<ActionResult<List<UserSearchDto>>> FindUsers(string UserName)
    {

        try
        {
            var users = await _userservice.FindUsers(UserName);
            return Ok(users);
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }


    }


    [HttpPost("[Action]/{ReceiverId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SendMessage(int ReceiverId, MessageDto messageDto)
    {
        try
        
        {
            await _chatservice.SendMessgae(messageDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }



    }

    [HttpGet("[Action]/{OtherUser:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MessageDto>>> GetListOfMessages(int currenUser , int OtherUser)
    {
      

        try
        {   
            var message = await _chatservice.GeTlistOfMessages(currenUser , OtherUser);
            return Ok(message);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }



    }


}
