
using System.Security.Cryptography;
using PasswordTheBest;
using SaBooBo.UserService.Domain.AggregatesModel;
using SaBooBo.UserService.Domain.Exceptions;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Application.Features.Commands;

public class RegisterUserCommandHandler(
    IUserRepository _userRepository
) : IRequestHandler<RegisterUserCommand, Guid>
{

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Pre check if user already exists
        var userExists = await _userRepository.GetByPhoneAsync(request.PhoneNumber);
        if (userExists is not null)
        {
            throw new UserAlreadyExistsException();
        }

        string passwordHash = PasswordTheBestFactory.Create(HashAlgorithmName.SHA256).Hash(
            request.Password,
            out string passwordSalt
        );

        var user = User.Create(
            phoneNumber: request.PhoneNumber,
            passwordHash: passwordHash,
            passwordSalt: passwordSalt
        );

        await _userRepository.CreateAsync(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return user.Id;
    }
}
