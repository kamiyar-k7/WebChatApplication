using Blazored.LocalStorage;
using System.Net.Http.Headers;
using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public class HomePageServcies : IHomePageServcies
{

    #region Ctor

    private readonly IClient _client;
    private readonly ILocalStorageService _localstorage;



    public HomePageServcies(IClient client, ILocalStorageService localstorage)
    {
        _client = client;
        _localstorage = localstorage;
    }


    #endregion

    protected async Task GetBearerToken()
    {
    var token = await _localstorage.GetItemAsync<string>("token");
        if (token != null)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        }


    }

    public async Task<List<OtherUserDto>> SerchUsers(string username)
    {
        await GetBearerToken();
        var users = await _client.FindUsersAsync(username);
        return users.ToList();

    }

 


    public async Task<List<ConversationDto>> GetConversations(int cid)
    {


        await GetBearerToken();
        var cons = await _client.GetUserConverstationsAsync(cid);
        if (cons != null)
        {
            return cons.ToList();
        }
        else
        {
            return new List<ConversationDto>();
        }
     

     
    }



}
