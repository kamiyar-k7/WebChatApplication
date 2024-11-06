
using Application.ViewModel_And_Dto.Dto.UserSide;

namespace Application.Services.Interfaces;

public interface IChatServices
{

    #region Messages

    Task SaveMessage(MessageDto message);

    #endregion


    #region Convertation
    Task<int> GetConversationId(int user1Id, int user2Id);
    Task<int> CreateConverstation(int user1Id, int user2Id);
    Task<List<MessageDto>> GetConverstationMessages(int conid);
    Task<List<OtherUserDto>> GetUserConversations(int userId);

    #endregion
}
