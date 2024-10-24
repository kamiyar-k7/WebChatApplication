
using Doamin.Entities.ChatEntites;

namespace Application.ViewModel_And_Dto.Dto.UserSide;

public class MessageListDto
{
    public int ResiverId { get; set; }
    public int SenderId { get; set; }

    public List<Messages>? Messages { get; set; }

}
