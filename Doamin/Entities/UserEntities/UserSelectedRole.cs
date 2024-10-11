namespace Doamin.Entities.UserEntities;

public class UserSelectedRole
{
    public int Id { get; set; }

    // user
    public string UserName { get; set; }
    public int UserId {  get; set; }

    //role
    public int RoleId { get; set; }
    public string RoleName { get; set; }

    // rels

    public User User { get; set; }
    public Role Role {  get; set; }

}