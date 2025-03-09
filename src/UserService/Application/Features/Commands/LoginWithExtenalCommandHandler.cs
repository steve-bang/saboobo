
using System.Security.Cryptography;
using System.Text.Json;
using Newtonsoft.Json;
using PasswordTheBest;
using SaBooBo.UserService.Application.Auth;
using SaBooBo.UserService.Domain.AggregatesModel;
using SaBooBo.UserService.Domain.Exceptions;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Application.Features.Commands;

public class LoginWithExtenalCommandHandler(
    IUserExternalProviderRepository userExternalProviderRepository,
    IUserRepository userRepository,
    IJwtHandler jwtHandler
) : IRequestHandler<LoginWithExtenalCommand, LoginUserResponse>
{
    private readonly string _defaultPassword = "123456";

    public Task<LoginUserResponse> Handle(LoginWithExtenalCommand request, CancellationToken cancellationToken)
    {
        switch (request.Provider)
        {
            case ExternalProviderAccount.Zalo:
                return HandleZalo(request.MerchantId, request.Metadata);
            case ExternalProviderAccount.Facebook:
                return HandleFacebook(request.Metadata);
            default:
                throw new NotImplementedException();
        }
    }

    private async Task<LoginUserResponse> HandleZalo(Guid merchantId, string metadata)
    {
        // Deserialize the metadata with camel case
        UserInfoZaloProvider? userInfoZalo = JsonConvert.DeserializeObject<UserInfoZaloProvider>(metadata);

        // Check if metadata is null
        if (userInfoZalo == null)
        {
            throw new ArgumentNullException(nameof(metadata));
        }
        
        // Check if user external provider exists
        var userExternalProvider = await userExternalProviderRepository.GetByUserExternalIdAsync(userInfoZalo.Id);

        User? user;

        // If userExternalProvider is null, create new user and userExternalProvider
        if (userExternalProvider == null)
        {
            // Find phone number by user external id
            user = await userRepository.GetByPhoneAsync(merchantId, userInfoZalo.PhoneNumber);

            if(user == null)
            {
                string passwordHash = PasswordTheBestFactory.Create(HashAlgorithmName.SHA256).Hash(
                    _defaultPassword,
                    out string passwordSalt
                );

                user = User.Create(
                    merchantId,
                    userInfoZalo.Name,
                    userInfoZalo.PhoneNumber,
                    passwordHash,
                    passwordSalt,
                    userInfoZalo.Avatar
                );
                user = await userRepository.CreateAsync(user);
            }

            // Create user external provider and save to database
            userExternalProvider = UserExtenalProvider.Create(
                userInfoZalo.Id,
                ExternalProviderAccount.Zalo,
                user.Id
            );

            await userExternalProviderRepository.CreateAsync(userExternalProvider);
        }
        else
        {
            // Get user by user id
            user = await userRepository.GetByIdAsync(userExternalProvider.UserId);
        }

        // Check if user is null then throw exception
        if (user == null) throw new UserNotFoundException();

        // Update last login at
        user.UpdateLastLoginAt();

        // Save changes
        await userRepository.UnitOfWork.SaveChangesAsync();

        // Generate the token
        jwtHandler.GenerateToken(user, out var accessToken, out var refreshToken, out var expires);


        // Return the response
        return new LoginUserResponse(user.Id, accessToken, refreshToken, expires);
    }

    private Task<LoginUserResponse> HandleFacebook(string metadata)
    {
        throw new NotImplementedException();
    }
}