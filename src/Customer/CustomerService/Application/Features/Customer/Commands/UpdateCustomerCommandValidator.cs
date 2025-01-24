
namespace SaBooBo.CustomerService.Application.Features.Commands;

public class UpdateCustomerCommandValidator :AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithErrorCode("Customer_Id_Invalid")
            .WithMessage("The ID is required");

        RuleFor(x => x.EmailAddress)
            .EmailAddress()
            .WithErrorCode("Customer_EmailAddress_Invalid")
            .WithMessage("Email address is not valid");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10}$").WithMessage("Phone number is not valid")
            .WithErrorCode("Customer_PhoneNumber_Invalid");

        RuleFor(x => x.AvatarUrl)
            .Matches(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)").WithMessage("Avatar URL is not valid")
            .WithErrorCode("Customer_AvatarUrl_Invalid");
    }
}