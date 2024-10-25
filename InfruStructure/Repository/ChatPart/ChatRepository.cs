
using Doamin.Entities.ChatEntites;
using Doamin.IRepository.ChatPart;
using InfruStructure.WebChatDbContext;
using Microsoft.EntityFrameworkCore;
using Doamin.Entities.UserEntities;

namespace InfruStructure.Repository.ChatPart;

public class ChatRepository : IChatRepository
{


    #region Ctor
    private readonly ChatDbContext _context;
    public ChatRepository(ChatDbContext chatDbContext)
    {
        _context = chatDbContext;
    }
    #endregion



    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }


    public async Task AddMessage(Messages messages)
    {
        await _context.Messages.AddAsync(messages);
        await SaveChanges();
    }

    public async Task<List<Messages>> GetMessagesBetweenUsers(int currenUser, int OtherUser)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Resiver)
            .Where(m =>
                (m.SenderId == currenUser && m.ResiverId == OtherUser) ||
                (m.SenderId == OtherUser && m.ResiverId == currenUser))
            .OrderBy(m => m.Timestamp)
            .AsQueryable().AsNoTracking().ToListAsync();
    }



}
