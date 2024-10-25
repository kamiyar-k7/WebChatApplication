using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public interface IPrivateChatService
{
    Task SendMessage(MessageDto messageDto);
    Task<List<MessageDto>> GetListOfMessages(int cuurentUser, int otherUser);
}
