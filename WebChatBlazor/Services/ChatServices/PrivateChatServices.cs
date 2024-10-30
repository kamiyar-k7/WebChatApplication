using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using WebChatBlazor.Services.AuthServices;
using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public class PrivateChatServices : IPrivateChatService
{

    #region Ctor

    private readonly IClient _client;
    private readonly IUserProvider _userProvider;
    public PrivateChatServices(IClient client, IUserProvider userProvider)
    {
        _client = client;
        _userProvider = userProvider;
    }
    #endregion

    public int CurrentUserId()
    {
     var userr =   _userProvider.SetCurrentUserFromClaims();
        return userr.Id;
    }


    public async Task<List<MessageDto>> GetListOfMessages(int cuurentUser, int otherUser)
    {
        var messages = await _client.GetListOfMessagesAsync(cuurentUser, otherUser);

     
        return messages.ToList();
    }

    public async Task<bool> IsConversationExist(int cuurentUser, int otherUser)
    {
        return  await _client.IsConverstationExistAsync(cuurentUser, otherUser);

    }

    public async Task CreateConverstation(int otherUser)
    {
        var cid = CurrentUserId();
        await _client.CreateConverstationAsync(cid, otherUser);
    }

   

}
