
using System.Security.Cryptography;
using PasswordTheBest;
using PhoneNumbers;
using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.UserService.Application.Clients;
using SaBooBo.UserService.Domain.AggregatesModel;
using SaBooBo.UserService.Domain.Exceptions;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Application.Features.Commands;

public class RegisterUserCommandHandler(
    IUserRepository _userRepository,
    IMerchantClient _merchantClient
) : IRequestHandler<RegisterUserCommand, Guid>
{

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (request.MerchantId.HasValue){
            
            var merchant = await _merchantClient.GetMerchantByIdAsync(request.MerchantId.Value);

            if (merchant is null)
            {
                throw new NotFoundException(
                    "Merchant_Not_Found",
                    $"Merchant with id {request.MerchantId} not found",
                    "The merchant you are trying to register with does not exist, please check the merchant id and try again."
                );
            }
        }

        // Convert phone number to E.164 format
        var phoneNumberNational = PhoneNumberUtil.GetInstance().Parse(request.PhoneNumber, defaultRegion: "VN").NationalNumber.ToString();

        // Pre check if user already exists
        var userExists = await _userRepository.GetByPhoneAsync(phoneNumberNational);
        if (userExists is not null)
        {
            throw new UserAlreadyExistsException();
        }

        string passwordHash = PasswordTheBestFactory.Create(HashAlgorithmName.SHA256).Hash(
            request.Password,
            out string passwordSalt
        );

        var user = User.Create(
            merchantId: request.MerchantId,
            phoneNumber: request.PhoneNumber,
            passwordHash: passwordHash,
            passwordSalt: passwordSalt,
            name: request.Name
        );

        await _userRepository.CreateAsync(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return user.Id;
    }
}
