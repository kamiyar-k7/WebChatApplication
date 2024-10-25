using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public class HomePageServcies : IHomePageServcies
{

    private readonly IClient _client;

    public HomePageServcies(IClient client)
    {
        _client = client;
    }


    public async Task<List<UserSearchDto>>  SerchUsers(string username)
    {
        var users = await _client.FindUsersAsync(username);
        return users.ToList();

    }


}
