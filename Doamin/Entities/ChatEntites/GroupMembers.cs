
using Doamin.Entities.UserEntities;

namespace Doamin.Entities.ChatEntites;
public class GroupMember
{
    public required Guid GroupId { get; set; }
    public Group? Group { get; set; }
    public string UserId { get; set; }
    public User? User { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime? ChatHistoryTime { get; set; }

}