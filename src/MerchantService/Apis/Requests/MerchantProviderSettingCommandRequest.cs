
namespace SaBooBo.MerchantService.Apis.Requests;

public record MerchantProviderSettingCommandRequest(
    MerchantProviderType ProviderType,
    IDictionary<string, string> Metadata
);