using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using SaBooBo.Domain.Shared.Configurations;
using SaBooBo.UserService.Grpc;

namespace SaBooBo.Clients.Shared.Clients;

public interface IUserClient
{
    Task<UserExternalProviderResponse> GetExternalDataByUserId(string userId);
}

public class UserClient : IUserClient
{
    private readonly UserGrpc.UserGrpcClient _client;
    private readonly ILogger<UserClient> _logger;

    public UserClient(ServiceConfiguration config, ILogger<UserClient> logger)
    {
        var channel = GrpcChannel.ForAddress(config.ServiceEndpoints.UserGrpc);
        _client = new UserGrpc.UserGrpcClient(channel);
        _logger = logger;
    }

    public async Task<UserExternalProviderResponse> GetExternalDataByUserId(string userId)
    {
        var request = new GetExternalDataByUserIdRequest { UserId = userId };
        return await _client.GetExternalDataByUserIdAsync(request);
    }
}