using Doamin.Entities.UserEntities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;



namespace Doamin.Entities.ChatEntites;

public class Messages
{

    public int Id { get; set; }


    public int SenderId { get; set; }
    public User Sender { get; set; }

    public int ResiverId { get; set; }
    public User Resiver { get; set; }

    public string? Content { get; set; }

    public DateTime Timestamp { get; set; }


}

