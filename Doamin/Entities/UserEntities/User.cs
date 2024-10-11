namespace Doamin.Entities.UserEntities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public string RoleName { get; set; }



    // rels 
    public ICollection<UserSelectedRole> userSelectedRoles { get; set; }


}
