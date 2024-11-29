using Blazored.LocalStorage;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.Net.Http.Headers;
using WebChatBlazor.Services.AuthServices;
using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public class PrivateChatServices : IPrivateChatService
{

    #region Ctor

    private readonly IClient _client;
    private readonly ILocalStorageService _localstorage;
    public PrivateChatServices(IClient client, ILocalStorageService localstorage)
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

    public async Task<OtherUserDto> GetOtherUserDto(int id)
    {

        await GetBearerToken();
        return await _client.GetOtherUserDetailsAsync(id);
    }


    public async Task<List<MessageDto>> GetListOfMessages(int conid, string id)
    {

        await GetBearerToken();
        var messages = await _client.GetListOfMessagesAsync(conid, id);


        return messages.ToList();
    }

    public async Task<int> IsConversationExist(int cuurentUser, int otherUser)
    {

        await GetBearerToken();
        return await _client.IsConverstationExistAsync(cuurentUser, otherUser);

    }

    public async Task<int> CreateConverstation(int cid, int otherUser)
    {

        await GetBearerToken();
        return await _client.CreateConverstationAsync(cid, otherUser);
    }

    public async Task SaveUnsentMessages(int currentuserId,List<MessageDto> messages)
    {

        await _localstorage.SetItemAsync($"unsentMessages_{currentuserId}", messages);
    }

    public async Task<List<MessageDto>> GetUnsentMessages(int currentuserId)
    {

        var messages = await _localstorage.GetItemAsync<List<MessageDto>>($"unsentMessages_{currentuserId}");
        return messages ?? new List<MessageDto>();
    }


}
