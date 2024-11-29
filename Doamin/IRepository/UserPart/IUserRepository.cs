using Doamin.Entities.UserEntities;

namespace Doamin.IRepository.UserPart;

public interface IUserRepository
{
    #region Auth

    Task AddUser(User user);
    Task<bool> IsEmailExist(string email);
    Task<bool> IsUserNameExist(string username);
    Task<User?> SignIn(string email, string password);

    #endregion

    Task<List<User>> FindUsers(string UserName);

    Task<User?> GetOtherUserDetails(int id);
}
