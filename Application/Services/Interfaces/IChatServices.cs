
using Application.ViewModel_And_Dto.Dto.UserSide;

namespace Application.Services.Interfaces;

public interface IChatServices
{

    #region Messages

    Task SaveMessage(MessageDto message);

    #endregion


    #region Convertation
    Task<bool> IsCoverstationExist(int user1Id, int user2Id);
    Task CreateConverstation(int user1Id, int user2Id);
    Task<List<MessageDto>> GetConverstationMessages(int currentUser, int OtherUser);
    Task<List<OtherUserDto>> GetUserConversations(int userId);

    #endregion
}
