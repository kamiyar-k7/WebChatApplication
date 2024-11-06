
namespace Doamin.Entities.ChatEntites;

public class Group
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }

    // Navigation property for GroupMembers
    public List<GroupMember>? Members { get; set; }

    // Navigation property for Messages
    public List<Messages>? Messages { get; set; }
}