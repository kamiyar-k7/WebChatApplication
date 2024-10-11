

using Doamin.Entities.UserEntities;

namespace Doamin.IRepository.UserPart;

public interface IRoleRepository
{
    Task AddRole(Role role);
    Task<Role> GetRoleName(string rolename);
}
