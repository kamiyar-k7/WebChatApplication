namespace Doamin.Entities.UserEntities;

public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; }

    // rels
    public ICollection<UserSelectedRole>? userSelectedRoles { get; set; }

}
