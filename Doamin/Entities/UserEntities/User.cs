using Doamin.Entities.ChatEntites;

namespace Doamin.Entities.UserEntities;

public class User
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfileBio { get; set; }
    public string? UserEmail { get; set; }
    public string? Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ProfileImageUrl { get; set; }
    public bool IsOnline { get; set; }

    // rels 
    public string? RoleName { get; set; }
    public ICollection<UserSelectedRole> userSelectedRoles { get; set; }
    public ICollection<Messages> Messages { get; set; }

}
