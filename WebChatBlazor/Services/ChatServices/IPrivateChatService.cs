using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public interface IPrivateChatService
{
    Task<List<MessageDto>> GetListOfMessages(int cuurentUser, int otherUser);
    Task<bool> IsConversationExist(int cuurentUser, int otherUser);
    Task CreateConverstation(int otherUser);
}
