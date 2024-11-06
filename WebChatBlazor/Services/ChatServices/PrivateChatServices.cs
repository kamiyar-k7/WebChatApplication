using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using WebChatBlazor.Services.AuthServices;
using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public class PrivateChatServices : IPrivateChatService
{

    #region Ctor

    private readonly IClient _client;
  
    public PrivateChatServices(IClient client)
    {
        _client = client;
        
    }
    #endregion

   

    public async Task<OtherUserDto> GetOtherUserDto(int id)
    {
        return await _client.GetOtherUserDetailsAsync(id);
    }


    public async Task<List<MessageDto>> GetListOfMessages(int conid , string id)
    {   
        var messages = await _client.GetListOfMessagesAsync(conid,id );

     
        return messages.ToList();
    }

    public async Task<int> IsConversationExist(int cuurentUser, int otherUser)
    {
          return  await _client.IsConverstationExistAsync(cuurentUser, otherUser);
     
    }

    public async Task<int> CreateConverstation(int cid ,int otherUser)
    {
       
        return   await _client.CreateConverstationAsync(cid, otherUser);
    }

   

}
