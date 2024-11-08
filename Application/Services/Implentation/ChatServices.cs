using Application.Services.Interfaces;
using Application.ViewModel_And_Dto.Dto.UserSide;
using Doamin.Entities.ChatEntites;
using Doamin.IRepository.ChatPart;
using Doamin.Entities.UserEntities;

namespace Application.Services.Implentation;

public class ChatServices : IChatServices
{


    #region ctor
    private readonly IChatRepository _chatRepo;
    private readonly IConverstationRepository _converstationRepo;

    public ChatServices(IChatRepository chatRepo, IConverstationRepository converstationRepository)
    {
        _chatRepo = chatRepo;
        _converstationRepo = converstationRepository;
    }
    #endregion

    #region messages

    public async Task SaveMessage(MessageDto message)
    {

        var newmessage = new Messages()
        {
            Content = message.Content,
            ResiverId = message.ResiverId,
            SenderId = message.SenderId,
            Timestamp = DateTime.Now,
            ConversationId = message.ConverstationId,
           
        };

        await _chatRepo.AddMessage(newmessage);

    }



    #endregion

    #region Converstation 

    public async Task<int> GetConversationId(int user1Id, int user2Id)
    {

        return await _converstationRepo.GetCoversationId(user1Id, user2Id);
    }

    public async Task<int> CreateConverstation(int user1Id, int user2Id)
    {

        Conversation converstation = new Conversation()
        {
            User1Id = user1Id,
            User2Id = user2Id,
        };

        var id = await _converstationRepo.CreateConverstation(converstation);
        return id;
    }

    public async Task<List<MessageDto>> GetConverstationMessages(int conid)
    {

        var meesages = await _converstationRepo.GetMessageConverstation(conid);

        List<MessageDto> messagelist = new List<MessageDto>();

        foreach (var message in meesages)
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

    public async Task<List<ConversationDto>> GetUserConversations(int userId)
    {

        var ConDetailsList = await _converstationRepo.GetConverstationDetails(userId);

        if (ConDetailsList != null && ConDetailsList.Any())
        {
            

            List<ConversationDto> cons = new List<ConversationDto>();

            foreach (var details in ConDetailsList)
            {
                var otherUser = details.User1 ?? details.User2;
                var otherUserId = otherUser?.Id ?? userId; 
                var otherUserName = otherUser?.UserName ?? "Saved Messages";


                ConversationDto condetails = new ConversationDto()
                {
                    ConversationId = details.Id,
                    LastMessage = details?.messages?.FirstOrDefault()?.Content,
                    OtherUserId = otherUserId,
                    UserName = otherUserName,
                    LastMessageTimestamp = details.messages.FirstOrDefault().Timestamp,
                    

                };
                cons.Add(condetails);
            };


         return cons = cons.OrderByDescending(c => c.LastMessageTimestamp).ToList();
        }
        else
        {
            return new List<ConversationDto>();
        }
    }
    
    #endregion


}
