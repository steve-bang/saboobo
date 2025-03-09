
namespace SaBooBo.UserService.Application.Features.Commands;

public record DeleteUserAddressCommand(
    Guid UserId,
    Guid Id
) : IRequest<bool>;