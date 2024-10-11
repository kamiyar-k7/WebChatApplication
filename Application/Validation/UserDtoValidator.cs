

using Application.ViewModel_And_Dto.Dto.UserSide;
using FluentValidation;

namespace Application.Validation;

public class UserDtoValidator : AbstractValidator<object>
{

    public UserDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Continue;



        // signup
        When(user => user is UserSignUpDto, () =>
        {
            RuleFor(user => (user as UserSignUpDto).UserName).MaximumLength(20).WithMessage("Enter maximum 20 Chracters").MinimumLength(3).WithMessage("Enter At Least 3 Chracters").NotEmpty().WithMessage("This Field Cant be Empty");

            RuleFor(user => (user as UserSignUpDto).UserEmail).EmailAddress().
          WithMessage("The Email Format Is Not Correct").NotEmpty().WithMessage("This Field Cant be Empty");

            RuleFor(user => (user as UserSignUpDto).Password)
                .NotEmpty().WithMessage("Password is required for sign-up.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
        });

        //  UserSignInDto
        When(user => user is UserSignInDto, () =>
        {
            RuleFor(user => (user as UserSignInDto).UserEmail).EmailAddress().
            WithMessage("The Email Format Is Not Correct").NotEmpty().WithMessage("This Field Cant be Empty");


            RuleFor(user => (user as UserSignInDto).Password)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .NotEmpty().WithMessage("Password is required for sign-in.");


        });



    }
}
