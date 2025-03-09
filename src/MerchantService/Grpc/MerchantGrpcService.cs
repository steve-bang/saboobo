using Grpc.Core;

namespace SaBooBo.MerchantService.Grpc;

public class MerchantGrpcService : MerchantGrpc.MerchantGrpcBase
{
    private readonly IMerchantRepository _merchantRepository;
    private readonly ILogger<MerchantGrpcService> _logger;

    public MerchantGrpcService(
        IMerchantRepository merchantRepository,
        ILogger<MerchantGrpcService> logger)
    {
        _merchantRepository = merchantRepository;
        _logger = logger;
    }

    public async Task<MerchantResponse> GetMerchant(GetMerchantRequest request, ServerCallContext context)
    {
        try
        {
            var merchant = await _merchantRepository.GetByIdAsync(Guid.Parse(request.Id));

            if (merchant == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Merchant with ID {request.Id} not found"));
            }

            return new MerchantResponse
            {
                Id = merchant.Id.ToString(),
                UserId = merchant.UserId.ToString(),
                Name = merchant.Name,
                PhoneNumber = merchant.PhoneNumber,
                EmailAddress = merchant.EmailAddress,
                Address = merchant.Address,
                Code = merchant.Code,
                Description = merchant.Description,
                OaUrl = merchant.OAUrl,
                CreatedAt = merchant.CreatedAt.ToString(),
            };
        }
        catch (RpcException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving merchant {MerchantId}", request.Id);
            throw new RpcException(new Status(StatusCode.Internal, "An error occurred while retrieving the merchant"));
        }
    }
}