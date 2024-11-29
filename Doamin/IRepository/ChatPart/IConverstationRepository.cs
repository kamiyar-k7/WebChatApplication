
using Doamin.Entities.ChatEntites;
using Doamin.Entities.UserEntities;

namespace Doamin.IRepository.ChatPart;

public interface IConverstationRepository
{

    Task<int> GetCoversationId(int user1Id, int user2Id);

    Task<int> CreateConverstation(Conversation converstation);

    Task<List<Messages>> GetMessageConverstation(int conid);

    Task<List<Conversation>> GetConverstationDetails(int userId);
 
}
