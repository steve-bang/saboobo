
namespace SaBooBo.UserService.Application.Features.Commands;

public record RegisterUserCommand(
    string PhoneNumber,
    string Password,
    string ConfirmPassword
) : IRequest<Guid>;