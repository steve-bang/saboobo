
namespace SaBooBo.MerchantService.Application.Features.Queries;

public class GetMerchantProviderSettingByMerchantIdQueryHandler(
    IMerchantProviderSettingRepository _merchantProviderSettingRepository
) : IRequestHandler<GetMerchantProviderSettingByMerchantIdQuery, MerchantProviderSetting>
{

    public async Task<MerchantProviderSetting> Handle(GetMerchantProviderSettingByMerchantIdQuery request, CancellationToken cancellationToken)
    {
        var merchantProviderSetting = await _merchantProviderSettingRepository.GetByMerchantIdAsync(request.MerchantId);
        
        // Check if the merchant provider setting exists and the merchant id matches
        // If not, throw an exception
        if (merchantProviderSetting == null)
        {
            throw new MerchantProviderSettingNotFoundException(request.MerchantId);
        }

        return merchantProviderSetting;
    }
}