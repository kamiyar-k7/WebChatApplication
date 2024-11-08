
namespace Application.ViewModel_And_Dto.Dto.UserSide;

public class ConversationDto
{
    public int ConversationId { get; set; }
    public string? LastMessage { get; set; }
    public DateTime LastMessageTimestamp { get; set; }
     // user
    public int OtherUserId { get; set; }
    public string? UserName { get; set; }

}
