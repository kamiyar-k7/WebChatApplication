

using Application.Services.Implentation;
using Application.Services.Interfaces;
using Application.ViewModel_And_Dto.Dto.UserSide;
using Doamin.Entities.ChatEntites;
using Microsoft.AspNetCore.SignalR;

namespace Application.SignalR;

public class ChatSignalR : Hub
{

	#region Ctor

	private readonly IChatServices _chatservices;

    public ChatSignalR(IChatServices chatservices)
    {
        _chatservices = chatservices;
    }

    #endregion

    public async Task SendMessage(MessageDto messageDto)
    {
        // Save the message using your existing service
        await _chatservices.SaveMessage(messageDto);

        // Broadcast the message to the clients
        await Clients.All.SendAsync("ReceiveMessage", messageDto);
    }


}
