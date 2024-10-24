using Application.Services.Interfaces;
using Application.ViewModel_And_Dto.Dto.UserSide;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebChatApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ChatController : ControllerBase
{


    #region Ctor
    private readonly IChatServices _chatservice;

    public ChatController(IChatServices chatservice)
    {
        _chatservice = chatservice;
    }

    #endregion


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

    [HttpGet("[Action]/{OtherUserId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Chat(int OtherUserId )
    {
        int currentuser = 1;

        try
        {
            var message = await _chatservice.GeTlistOfMessages(currentuser, OtherUserId);
            return Ok(message);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        

        
    }


}
