
using System.Security.Cryptography;
using PasswordTheBest;
using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.UserService.Application.Auth;
using SaBooBo.UserService.Domain.Exceptions;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Application.Features.Commands;

public class LoginUserCommandHandler(
    IUserRepository _userRepository,
    IJwtHandler _jwtHandler
) : IRequestHandler<LoginUserCommand, LoginUserResponse>
{

    public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Get the user by phone number
        var user = await _userRepository.GetByPhoneAsync(request.PhoneNumber);

        if (user == null) throw new LoginFailedException();

        // Verify the password
        bool verifyPassword = PasswordTheBestFactory.Create(HashAlgorithmName.SHA256)
            .Verify(request.Password, user.PasswordHash, user.PasswordSalt);

        user.UpdateLastLoginAt();

        if (!verifyPassword) throw new LoginFailedException();

        // Generate the token
        _jwtHandler.GenerateToken(user, out var accessToken, out var refreshToken, out var expires);

        _userRepository.UpdateAsync(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        // Return the response
        return new LoginUserResponse(user.Id, accessToken, refreshToken, expires);
    }
}