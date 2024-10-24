using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public class PrivateChatServices : IPrivateChatService
{
    private readonly IClient _client;

    public PrivateChatServices(IClient client)
    {
        _client = client;
    }


    public async Task SendMessage(MessageDto messageDto)
    {
        await   _client.SendMessageAsync(messageDto.ResiverId , messageDto);
    }

}
