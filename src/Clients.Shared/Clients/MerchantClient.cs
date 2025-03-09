using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using SaBooBo.Clients.Shared.Merchant;
using SaBooBo.Domain.Shared.Configurations;

namespace SaBooBo.Clients.Shared.Clients;

public interface IMerchantClient
{
    Task<MerchantResponse> GetMerchantAsync(string merchantId);
}

public class MerchantClient : IMerchantClient
{
    private readonly MerchantGrpc.MerchantGrpcClient _client;
    private readonly ILogger<MerchantClient> _logger;

    public MerchantClient(ServiceConfiguration config, ILogger<MerchantClient> logger)
    {
        var channel = GrpcChannel.ForAddress(config.ServiceEndpoints.MerchantGrpc);
        _client = new MerchantGrpc.MerchantGrpcClient(channel);
        _logger = logger;
    }

    public async Task<MerchantResponse> GetMerchantAsync(string merchantId)
    {
        var request = new GetMerchantRequest { Id = merchantId };
        return await _client.GetMerchantByIdAsync(request);
    }
} 