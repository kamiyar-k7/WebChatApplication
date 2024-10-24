﻿using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.ChatServices;

public interface IPrivateChatService
{
    Task SendMessage(MessageDto messageDto);
}