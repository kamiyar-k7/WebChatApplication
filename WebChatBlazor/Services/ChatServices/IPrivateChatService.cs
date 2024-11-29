using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public interface IPrivateChatService
{
    Task<List<MessageDto>> GetListOfMessages(int conid, string id);
    Task<int> IsConversationExist(int cuurentUser, int otherUser);
    Task<int> CreateConverstation(int cid, int otherUser);
    Task<OtherUserDto> GetOtherUserDto(int id);
    Task SaveUnsentMessages(int currentuserId, List<MessageDto> messages);
    Task<List<MessageDto>> GetUnsentMessages(int currentuserId);
}
