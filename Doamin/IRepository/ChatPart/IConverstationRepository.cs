
using Doamin.Entities.ChatEntites;
using Doamin.Entities.UserEntities;

namespace Doamin.IRepository.ChatPart;

public interface IConverstationRepository
{

    Task<bool> IsConverstationExist(int user1Id, int user2Id);

    Task CreateConverstation(Converstation converstation);

    Task<List<Messages>> GetMessageConverstation(int user1Id, int user2Id);

    Task<List<int>> GetIdOFOtherUserInConversation(int userId);

    Task<List<User>> GetUserConversations(List<int> ids);
}
