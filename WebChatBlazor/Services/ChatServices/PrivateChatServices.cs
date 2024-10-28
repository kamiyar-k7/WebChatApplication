using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public class PrivateChatServices : IPrivateChatService
{
    private readonly IClient _client;

    public PrivateChatServices(IClient client)
    {
        _client = client;
    }
    public async Task<List<MessageDto>> GetListOfMessages(int cuurentUser, int otherUser)
    {
        var messages = await _client.GetListOfMessagesAsync(cuurentUser, otherUser);

        // Return the fetched messages.
        return messages.ToList();
    }



   

}
