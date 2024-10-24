
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
            Timestamp  = DateTime.UtcNow,

        };

        await _chatRepo.AddMessage(newmessage);

    }

    public async Task<List<MessageDto>> GeTlistOfMessages(int currentUser , int OtherUser)
    {
        
        var messages = await _chatRepo.GetMessagesBetweenUsers(currentUser, OtherUser);

        List<MessageDto> messagelist = new List<MessageDto>();

        foreach (var message in messages)
        {
            MessageDto messageDto = new MessageDto()
            {
                Content = message.Content,
                Timestamp = message.Timestamp,
                SenderId= message.SenderId,
                ResiverId = message.ResiverId,
             
                
            };

            messagelist.Add(messageDto);    

        }
               
        return messagelist;
        
    }
    #endregion



}
