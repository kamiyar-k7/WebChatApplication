

namespace Application.ViewModel_And_Dto.Dto.UserSide;

public record UserSignInDto
{

    public string? UserEmail { get; set; }
    public string? Password { get; set; }
}
