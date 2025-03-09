
namespace SaBooBo.UserService.Application.Features.Commands;

public record RegisterUserCommand(
    Guid? MerchantId,
    string PhoneNumber,
    string Password,
    string ConfirmPassword,
    string Name
) : IRequest<Guid>;