
namespace SaBooBo.UserService.Application.Features.Commands;

public record LoginUserCommand(string PhoneNumber, string Password) : IRequest<LoginUserResponse>;

public record LoginUserResponse(
    Guid UserId,
    string AccessToken, 
    string RefreshToken,
    DateTime ExpiresIn
);