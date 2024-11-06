using Doamin.Entities.UserEntities;

namespace Doamin.IRepository.UserPart;

public interface IUserRepository
{
    #region Auth

    Task AddUser(User user);
    Task<bool> IsExist(string email);
    Task<User?> SignIn(string email, string password);

    #endregion

    Task<List<User>> FindUsers(string UserName);

    Task<User?> GetOtherUserDetails(int id);
}
