
using SaBooBo.Domain.Shared.Utils;
using SaBooBo.UserService.Domain.Exceptions;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Grpc;

public class UserGrpcService : UserGrpc.UserGrpcBase
{
    private readonly IUserExternalProviderRepository _userExternalProviderRepository;

    public UserGrpcService(
        IUserExternalProviderRepository merchantRepository
    )
    {
        _userExternalProviderRepository = merchantRepository;
    }


    public async Task<UserExternalProviderResponse> GetExternalDataByUserId(string userId)
    {
        var userExternalProvider = await _userExternalProviderRepository.GetByUserIdAsync(Guid.Parse(userId));

        if (userExternalProvider == null)
        {
            LoggingUtil.WriteLog($"User External Provider with ID {userId} not found");
            throw new UserNotFoundException();
        }

        return new UserExternalProviderResponse
        {
            Id = userExternalProvider.Id.ToString(),
            UserId = userExternalProvider.UserId.ToString(),
            UserExternalId = userExternalProvider.UserExternalId,
            Provider = userExternalProvider.Provider.ToString(),
            CreatedAt = userExternalProvider.CreatedAt.ToString(),
        };
    }

}