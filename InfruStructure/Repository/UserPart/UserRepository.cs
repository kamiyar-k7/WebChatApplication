

using Doamin.Entities.UserEntities;
using Doamin.IRepository.UserPart;
using InfruStructure.WebChatDbContext;

namespace InfruStructure.Repository.UserPart;

public class UserRepository : IUserRepository
{
    private readonly ChatDbContext _DbContext;
    #region Ctor
    public UserRepository(ChatDbContext chatDbContext)
    {
        _DbContext = chatDbContext;
    }
    #endregion

    public async Task SaveChanges()
    {
        await _DbContext.SaveChangesAsync();
    }

    public async Task AddUser(User user)
    {
        await _DbContext.Users.AddAsync(user);
        await SaveChanges();
    }
    

}
