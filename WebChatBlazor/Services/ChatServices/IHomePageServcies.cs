using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public interface IHomePageServcies
{
    Task<List<UserSearchDto>> SerchUsers(string username);

}
