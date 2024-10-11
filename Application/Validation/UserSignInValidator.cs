

using Application.ViewModel_And_Dto.Dto.UserSide;
using FluentValidation;

namespace Application.Validation;

public class UserSignInValidator : AbstractValidator<UserSignInDto>
{

    public UserSignInValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Continue;

       
        RuleFor(x => x.UserEmail).EmailAddress().WithMessage("The Email Format Is Not Correct").NotEmpty().WithMessage("This Field Cant be Empty");
        RuleFor(x => x.Password).MinimumLength(8).WithMessage("Enter At Least 8 Chracters").NotEmpty().WithMessage("This Field Cant be Empty");

    }
}
