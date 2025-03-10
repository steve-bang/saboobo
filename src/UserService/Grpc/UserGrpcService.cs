
using Grpc.Core;
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

    public override async Task<UserExternalProviderResponse> GetExternalDataByUserId(GetExternalDataByUserIdRequest request, ServerCallContext context)
    {
        var userExternalProvider = await _userExternalProviderRepository.GetByUserIdAsync(Guid.Parse(request.UserId));

        if (userExternalProvider == null)
        {
            LoggingUtil.WriteLog($"User External Provider with ID {request.UserId} not found");
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