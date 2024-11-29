using Doamin.Entities.ChatEntites;
using Doamin.Entities.UserEntities;
using Doamin.IRepository.ChatPart;
using InfruStructure.WebChatDbContext;
using Microsoft.EntityFrameworkCore;

namespace InfruStructure.Repository.ChatPart;

public class ConverstationRepository : IConverstationRepository
{

    #region Ctor
    private readonly ChatDbContext _Context;

    public ConverstationRepository(ChatDbContext context)
    {
        _Context = context;
    }

    #endregion


    public async Task<int> GetCoversationId(int user1Id, int user2Id)
    {

        return await _Context.conversations.Where(c => (c.User1Id == user1Id && c.User2Id == user2Id) || (c.User1Id == user2Id && c.User2Id == user1Id)).Select(x => x.Id).FirstOrDefaultAsync();

    }

    public async Task<int> CreateConverstation(Conversation converstation)
    {

        await _Context.conversations.AddAsync(converstation);
        await _Context.SaveChangesAsync();
        return converstation.Id;

    }

    public async Task<List<Messages>> GetMessageConverstation(int conid)
    {


        return await _Context.Messages.Where(x => x.ConversationId == conid).Include(x => x.Sender).Select(x => new Messages
        {
            Id = x.Id,
            Content = x.Content,
            ConversationId = conid,
            Timestamp = x.Timestamp,
            IsSend = x.IsSend ,
            IsSeen = x.IsSeen,
            Sender = new User
            {
                UserEmail = x.Sender.UserEmail,
                UserName = x.Sender.UserName,
                CreatedAt = DateTime.Now,
                RoleName = x.Sender.RoleName,
                Id = x.Id,

            },
            SenderId = x.SenderId,

            Resiver = new User
            {
                UserEmail = x.Sender.UserEmail,
                UserName = x.Sender.UserName,
                CreatedAt = DateTime.Now,
                RoleName = x.Sender.RoleName,
                Id = x.Id,
            },
            ResiverId = x.ResiverId



        }).ToListAsync();
    }



    public async Task<List<Conversation>> GetConverstationDetails(int userId)
    {
        try
        {
            return await _Context.conversations.Where(c => c.User1Id == userId || c.User2Id == userId)
              .Select(c => new Conversation
              {
                      Id = c.Id,
                      messages = c.messages.OrderByDescending(m => m.Timestamp).Select(x => new Messages
                         {
                             Content = x.Content,
                             Timestamp = x.Timestamp
                             
                          }).Take(1).ToList(),

                          User1 = c.User1Id == userId ? null : new User { Id = c.User1.Id, UserName = c.User1.UserName },
                              User2 = c.User2Id == userId ? null : new User { Id = c.User2.Id, UserName = c.User2.UserName }

              })
               .ToListAsync();
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }



}
