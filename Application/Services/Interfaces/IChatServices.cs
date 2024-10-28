
using Application.ViewModel_And_Dto.Dto.UserSide;

namespace Application.Services.Interfaces;

public interface IChatServices
{
    Task SaveMessage(MessageDto message);
    Task<List<MessageDto>> GeTlistOfMessages(int currenUser, int OtherUser);
}
