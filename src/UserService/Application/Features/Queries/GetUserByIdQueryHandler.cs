
using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.UserService.Application.Features.Commands;
using SaBooBo.UserService.Application.Models;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Application.Features.Queries;

public class GetUserByIdQueryHandler(
    IUserRepository _userRepository
) : IRequestHandler<GetUserByIdQuery, UserResponse>
{

    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            throw new NotFoundException(
                "User_not_found",
                $"User with id {request.UserId} not found",
                "The user with the specified id was not found"
            );
        }

        return new UserResponse(
            id: user.Id,
            phoneNumber: user.PhoneNumber,
            name: user.Name,
            emailAddress: user.EmailAddress,
            avatarUrl: user.AvatarUrl,
            isActive: user.IsActive,
            lastLoginAt: user.LastLoginAt,
            createdAt: user.CreatedAt
        );
    }
}