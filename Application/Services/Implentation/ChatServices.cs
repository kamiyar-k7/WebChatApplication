
using Application.Services.Interfaces;
using Application.SignalR;
using Application.ViewModel_And_Dto.Dto.UserSide;
using Doamin.Entities.ChatEntites;
using Doamin.IRepository.ChatPart;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualBasic;

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
            Timestamp = DateTime.UtcNow,
            

        };

        await _chatRepo.AddMessage(newmessage);

    }



    #endregion

    #region Converstation Room

    public async Task<bool> IsCoverstationExist(int user1Id, int user2Id)
    {

        return await _converstationRepo.IsConverstationExist(user1Id, user2Id);
    }

    public async Task CreateConverstation(int user1Id, int user2Id)
    {

        Converstation converstation = new Converstation()
        {
            User1Id = user1Id,
            User2Id = user2Id,
        };

        await _converstationRepo.CreateConverstation(converstation);

    }

    public async Task<List<MessageDto>> GetConverstationMessages(int currentUser, int OtherUser)
    {

        var meesages = await _converstationRepo.GetMessageConverstation(currentUser, OtherUser);

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

    public async Task<List<OtherUserDto>> GetUserConversations(int userId)
    {

        var usersid = await _converstationRepo.GetIdOFOtherUserInConversation(userId);
        if (usersid != null)
        {
            var users = await _converstationRepo.GetUserConversations(usersid);

            List<OtherUserDto> cons = new List<OtherUserDto>();

            foreach (var userdetails in users)
            {

                OtherUserDto otherUserDto = new OtherUserDto()
                {
                    Id = userdetails.Id,
                    UserName = userdetails.UserName,

                };
                cons.Add(otherUserDto);

            }

            return cons;
        }
        else
        {
            return null;
        }




    }

    #endregion


}
