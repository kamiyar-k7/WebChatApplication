using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public class HomePageServcies
{

    private readonly IClient _client;

    public HomePageServcies(IClient client)
    {
        _client = client;
    }


    //public async Task<List<usersDto>> SerchUsers(string username)
    //{
    //    return null;
    //}


}
