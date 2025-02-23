
namespace SaBooBo.UserService.Application.Features.Commands;

public record RegisterUserCommand(
    Guid? MerchantId,
    string PhoneNumber,
    string Password,
    string ConfirmPassword
) : IRequest<Guid>;