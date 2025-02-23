
namespace SaBooBo.MerchantService.Application.Features.Queries
{
    public record GetMerchantProviderSettingByMerchantIdQuery(Guid MerchantId) : IRequest<MerchantProviderSetting>;
}