using Doamin.Entities.UserEntities;

namespace Doamin.IRepository.UserPart;

public interface IUserRepository
{
    Task AddUser(User user);
    Task<bool> IsExist(string email);
    Task<User?> SignIn(string email, string password);
}
