using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Security.Claims;
using WebChatBlazor.Services.AuthServices;
using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public class HomePageServcies : IHomePageServcies
{

    #region Ctor

    private readonly IClient _client;
   


    public HomePageServcies(IClient client)
    {
        _client = client;
      
    }


    #endregion
    

    public async Task<List<OtherUserDto>> SerchUsers(string username)
    {
        var users = await _client.FindUsersAsync(username);
        return users.ToList();

    }

    HubConnection _connection;

   

    public async Task<List<OtherUserDto>> GetConversations(int cid)
    {

       

         var cons = await _client.GetUserConverstationAsync(cid);

        return cons.ToList();

     
    }



}
