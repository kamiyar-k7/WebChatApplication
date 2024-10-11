

using Doamin.Entities.UserEntities;

namespace Doamin.Entities.Chats;

public class UserChatRoom
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public int ChatRoomId { get; set; }
    public ChatRoom ChatRoom { get; set; }
}
