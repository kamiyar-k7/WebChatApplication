﻿using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public interface IHomePageServcies
{
    Task<List<OtherUserDto>> SerchUsers(string username);
    Task<List<ConversationDto>> GetConversations(int cid);


}
