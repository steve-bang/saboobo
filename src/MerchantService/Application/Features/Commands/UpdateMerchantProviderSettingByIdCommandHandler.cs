
namespace MerchantService.Application.Features.Commands;

public class UpdateMerchantProviderSettingByIdCommandHandler(
    IMerchantRepository _merchantRepository,
    IMerchantProviderSettingRepository _merchantProviderSettingRepository
) : IRequestHandler<UpdateMerchantProviderSettingByIdCommand, Guid>
{

    public async Task<Guid> Handle(UpdateMerchantProviderSettingByIdCommand request, CancellationToken cancellationToken)
    {
        // Check if the merchant exists
        var merchant = await _merchantRepository.GetByIdAsync(request.MerchantId);
        if (merchant == null)
        {
            throw new MerchantNotFoundException(request.MerchantId);
        }

        // Check if the merchant provider setting exists
        var merchantProviderSetting = await _merchantProviderSettingRepository.GetByIdAsync(request.MerchantProviderSettingId);
        
        // Check if the merchant provider setting exists and belongs to the merchant.
        if (merchantProviderSetting == null || merchantProviderSetting.MerchantId != request.MerchantId)
        {
            throw new MerchantProviderSettingNotFoundException(request.MerchantProviderSettingId);
        }

        // Update the merchant provider setting
        merchantProviderSetting.Update(
            providerType: request.ProviderType,
            metadata: request.Metadata
        );

        // Save changes
        await _merchantProviderSettingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        // Return the id of the updated merchant provider setting
        return merchantProviderSetting.Id;
    }
}