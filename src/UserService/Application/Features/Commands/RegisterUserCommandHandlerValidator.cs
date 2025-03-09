

namespace SaBooBo.UserService.Application.Features.Commands
{
    public class RegisterUserCommandHandlerValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandHandlerValidator()
        {

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                // Matches 10 digits and contains country code
                .Matches(@"^\+?\d+$")
                .WithMessage("Phone number must be 10 digits")
                .WithErrorCode("PhoneNumber_Invalid");

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .MaximumLength(20)
                .WithMessage("Password must be 6-20 characters long.")
                .WithErrorCode("Password_Invalid")
                .Equal(x => x.ConfirmPassword)
                .WithMessage("Passwords do not match")
                .WithErrorCode("Password_Mismatch");

        }
    }
}