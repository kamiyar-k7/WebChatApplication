using Microsoft.AspNetCore.SignalR.Client;
using WebChatBlazor.Services.AuthServices;
using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public class HomePageServcies : IHomePageServcies
{

    #region Ctor

    private readonly IClient _client;
    private readonly IUserProvider _userProvider;


    public HomePageServcies(IClient client, IUserProvider userProvider)
    {
        _client = client;
        _userProvider = userProvider;
    }


    #endregion
    

    public async Task<List<OtherUserDto>> SerchUsers(string username)
    {
        var users = await _client.FindUsersAsync(username);
        return users.ToList();

    }

    HubConnection _connection;

    public async Task<List<OtherUserDto>> GetConversations()
    {

        var user = _userProvider.SetCurrentUserFromClaims();

        var cons = await _client.GetUserConverstationAsync(user.Id);

        return cons.ToList();

    }



}
