using Application.Services.Interfaces;
using Application.ViewModel_And_Dto.Dto.UserSide;
using Microsoft.AspNetCore.SignalR;

namespace Application.SignalR;

public class ChatSignalR : Hub
{

	#region Ctor

	private readonly IChatServices _chatservices;
    private readonly IUserServices _userServices;
    public ChatSignalR(IChatServices chatservices, IUserServices userServices)
    {
        _chatservices = chatservices;
        _userServices = userServices;
    }

    #endregion

    public async Task SendMessage(MessageDto messageDto)
    {
        // Save the message using your existing service
        await _chatservices.SaveMessage(messageDto);

        // Broadcast the message to the clients
        await Clients.All.SendAsync("ReceiveMessage" , messageDto);

        await Clients.All.SendAsync("GetConversations");
    }

    public async Task SearchUsers(string userName)
    {
        var searchResults = await _userServices.FindUsers(userName);
        await Clients.Caller.SendAsync("GetSearch", searchResults);
    }

}
