using Doamin.Entities.UserEntities;

namespace Doamin.Entities.ChatEntites;

public class Messages
{

    public int Id { get; set; }

    public int SenderId { get; set; }
    public User? Sender { get; set; }

    public int ResiverId { get; set; }
    public User? Resiver { get; set; }

    public string? Content { get; set; }

    public DateTime Timestamp { get; set; }

    public bool IsSend { get; set; }

    public bool IsSeen { get; set; }
    // seen
    // datetime seenDate

    //Coversation
    public int? ConversationId { get; set; }
    public Conversation? Conversation { get; set; }

}

