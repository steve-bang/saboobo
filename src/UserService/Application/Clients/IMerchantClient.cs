
namespace SaBooBo.UserService.Application.Clients;

public interface IMerchantClient
{
    Task<object?> GetMerchantByIdAsync(Guid merchantId);
}