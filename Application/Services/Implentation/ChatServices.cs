
using Application.Services.Interfaces;
using Application.ViewModel_And_Dto.Dto.UserSide;
using Doamin.Entities.ChatEntites;
using Doamin.IRepository.ChatPart;

namespace Application.Services.Implentation;

public class ChatServices : IChatServices
{
    private readonly IChatRepository _chatRepo;

    #region ctor

    public ChatServices(IChatRepository chatRepo)
    {
        _chatRepo = chatRepo;
    }

    public async Task SendMessgae(MessageDto message)
    {

        var newmessage = new Messages()
        {
            Content = message.Content,
            ResiverId = message.ResiverId,
            SenderId = message.SenderId,
            Timestamp = DateTime.UtcNow,

        };

        await _chatRepo.AddMessage(newmessage);

    }

    public async Task<List<MessageDto>> GeTlistOfMessages(int currenUser, int OtherUser)
    {

        var AllMessages = await _chatRepo.GetMessagesBetweenUsers(currenUser, OtherUser);

        List<MessageDto> messagelist = new List<MessageDto>();

        foreach (var message in AllMessages)
        {
            MessageDto messageDto = new MessageDto()
            {
                Id = message.Id,
                Content = message.Content,
                Timestamp = message.Timestamp,
                SenderId = message.SenderId,
                ResiverId = message.ResiverId,
                ResiverName = message.Resiver.UserName,
                SenderName = message.Sender.UserName,

            };

            messagelist.Add(messageDto);

        }

        return messagelist;

    }
    #endregion



}
