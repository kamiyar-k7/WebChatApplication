
using Doamin.Entities.ChatEntites;
using Doamin.IRepository.ChatPart;
using InfruStructure.WebChatDbContext;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Messages>> GetMessagesBetweenUsers(int currentUserId, int otherUserId)
    {
        return await _context.Messages
            .Where(m =>
                (m.SenderId == currentUserId && m.ResiverId == otherUserId) ||
                (m.SenderId == otherUserId && m.ResiverId == currentUserId))
            .OrderBy(m => m.Timestamp) // Optional: Order by the message timestamp
            .ToListAsync();
    }


}
