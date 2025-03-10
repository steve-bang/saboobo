using Grpc.Core;
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
    private readonly ILogger<MerchantClient> _logger;
    private readonly GrpcChannel _channel;
    private readonly MerchantGrpc.MerchantGrpcClient _client;

    public MerchantClient(ServiceConfiguration config, ILogger<MerchantClient> logger)
    {
        _logger = logger;

        var channelOptions = new GrpcChannelOptions
        {
            HttpHandler = new SocketsHttpHandler
            {
                EnableMultipleHttp2Connections = true,
                KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5)
            }
        };

        Console.WriteLine($"Service endpoint MerchantClientGRPC: {config.ServiceEndpoints.MerchantGrpc}");

        _channel = GrpcChannel.ForAddress(config.ServiceEndpoints.MerchantGrpc, channelOptions);
        _client = new MerchantGrpc.MerchantGrpcClient(_channel);
    }

    public async Task<MerchantResponse> GetMerchantAsync(string merchantId)
    {
        try
        {
            var request = new GetMerchantRequest { Id = merchantId };
            return await _client.GetMerchantByIdAsync(request);
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Unavailable)
        {
            _logger.LogError(ex, "gRPC service unavailable when getting merchant {MerchantId}. Service endpoint: {Endpoint}", 
                merchantId, _channel.Target);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting merchant {MerchantId}", merchantId);
            throw;
        }
    }

    ~MerchantClient()
    {
        _channel.Dispose();
    }
} 