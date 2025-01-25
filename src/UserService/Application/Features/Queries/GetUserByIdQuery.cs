
using SaBooBo.UserService.Application.Models;

namespace SaBooBo.UserService.Application.Features.Commands;

public record GetUserByIdQuery(Guid UserId) : IRequest<UserResponse>;
