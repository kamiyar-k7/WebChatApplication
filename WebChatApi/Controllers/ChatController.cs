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

    [HttpGet("[Action]/{UserName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<OtherUserDto>>> FindUsers(string UserName)
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
            await _chatservice.SaveMessage(messageDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }



    }



    #region Coverstation

    [HttpGet("[Action]/{user2Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> IsConverstationExist(int user1Id, int user2Id)
    {

        try
        {
            var res = await _chatservice.IsCoverstationExist(user1Id, user2Id);
            return Ok(res);

        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }


    [HttpPost("[Action]/{user2Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateConverstation(int  user1Id , int user2Id)
    {

        try
        {
            await _chatservice.CreateConverstation(user1Id , user2Id);
            return Ok();

        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);

        }
    }


    [HttpGet("[Action]/{UserId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<OtherUserDto>>> GetUserConverstation(int UserId)
    {
        try
        {
            var cons = await _chatservice.GetUserConversations(UserId);
            return Ok(cons);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("[Action]/{user2Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MessageDto>>> GetListOfMessages(int user1Id, int user2Id)
    {

        try
        {

            var message = await _chatservice.GetConverstationMessages(user1Id, user2Id);
            return Ok(message);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }

    #endregion


}
