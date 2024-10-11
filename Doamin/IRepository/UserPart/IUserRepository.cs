using Doamin.Entities.UserEntities;

namespace Doamin.IRepository.UserPart;

public interface IUserRepository
{
     Task AddUser(User user);
}
