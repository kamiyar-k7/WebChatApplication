using Application.ViewModel_And_Dto.Dto.UserSide;
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
            
            
    public async Task<bool> IsConverstationExist(int user1Id , int user2Id)
    {

        return await _Context.converstations.AnyAsync(c=> (c.User1Id == user1Id && c.User2Id == user2Id) ||(c.User1Id == user2Id && c.User2Id == user1Id));

    }

    public async Task CreateConverstation(Converstation converstation)
    {

        await _Context.converstations.AddAsync(converstation);
        await _Context.SaveChangesAsync();

    }

    public async Task<List<Messages>> GetMessageConverstation(int user1Id , int user2Id)
    {

        return await _Context.converstations.Where(c => (c.User1Id == user1Id && c.User2Id == user2Id) ||
        (c.User1Id == user2Id && c.User2Id == user1Id)
        ).SelectMany(x => x.messages).ToListAsync();

    }

    public async Task<List<int>> GetIdOFOtherUserInConversation(int userId)
    {
        return await _Context.converstations
                             .Where(c => c.User1Id == userId || c.User2Id == userId)
                             .Select(c => c.User1Id == userId ? c.User2Id : c.User1Id)
                             .ToListAsync();


    }

    public async Task<List<User>> GetUserConversations(List<int> ids)
    {
        return await _Context.Users.Where(user => ids.Contains(user.Id)).ToListAsync();
    }

}
