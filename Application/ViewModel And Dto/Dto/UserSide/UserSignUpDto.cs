
namespace Application.ViewModel_And_Dto.Dto.UserSide;

public record UserSignUpDto
{

    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public string? Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? RoleName { get; set; }

}
