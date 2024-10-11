namespace Doamin.Entities.Chats;

public class ChatRoom
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsGroup { get; set; } = false;
    public DateTime CreatedAt { get; set; }


    public ICollection<UserChatRoom>? UserChatRooms { get; set; } 
}
