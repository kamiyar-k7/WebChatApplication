


using Doamin.Entities.ChatEntites;

namespace Doamin.IRepository.ChatPart;

public interface IChatRepository
{
    Task SaveChanges();
    Task AddMessage(Messages messages);
    Task<List<Messages>> GetMessagesBetweenUsers(int currenUser, int OtherUser);
}
