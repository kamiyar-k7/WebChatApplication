
using Doamin.Entities.UserEntities;
using Doamin.IRepository.UserPart;
using InfruStructure.WebChatDbContext;
using Microsoft.EntityFrameworkCore;

namespace InfruStructure.Repository.UserPart;

public class RoleRepository : IRoleRepository
{
   

    private readonly ChatDbContext _DbContext;

    #region Ctor
    public RoleRepository(ChatDbContext chatDbContext)
    {
        _DbContext = chatDbContext;
    }
    #endregion

    public async Task SaveChanges()
    {
        await _DbContext.SaveChangesAsync();
    }

    public async Task AddRole(Role role)
    {
        await _DbContext.Roles.AddAsync(role);
        await SaveChanges();
    }
    public async Task<Role?> GetRoleName(string rolename)
    {
       return await _DbContext.Roles.Where(role => role.RoleName == rolename).FirstOrDefaultAsync() ;
    }

  
}
