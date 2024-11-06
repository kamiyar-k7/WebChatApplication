

using Doamin.Entities.UserEntities;

namespace Doamin.Entities.ChatEntites;

public class Converstation
{

    public int Id { get; set; }

    // user 1
    public int User1Id { get; set; }

    public User? User1 { get; set; }

    // user 2
    public int User2Id { get; set; }

    public User? User2 { get; set; }

    // messages between 2 users
    public List<Messages>? messages { get; set; }



}
