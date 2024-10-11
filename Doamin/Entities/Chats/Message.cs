using Doamin.Entities.UserEntities;

namespace Doamin.Entities.Chats;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }


    public int ChatRoomId { get; set; }
    public int SenderId { get; set; }
    public DateTime CreatedAt { get; set; }


    public User Sender { get; set; }
    public ChatRoom ChatRoom { get; set; }
}