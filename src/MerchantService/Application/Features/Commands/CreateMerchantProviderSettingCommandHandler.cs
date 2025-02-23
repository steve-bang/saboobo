

namespace MerchantService.Application.Features.Commands;

public class CreateMerchantProviderSettingCommandHandler(
    IMerchantRepository merchantRepository,
    IMerchantProviderSettingRepository merchantProviderSettingRepository
) : IRequestHandler<CreateMerchantProviderSettingCommand, Guid>
{

    public async Task<Guid> Handle(CreateMerchantProviderSettingCommand request, CancellationToken cancellationToken)
    {
        // Check if the merchant exists
        var merchant = await merchantRepository.GetByIdAsync(request.MerchantId);
        if (merchant == null)
        {
            throw new MerchantNotFoundException(request.MerchantId);
        }

        // Check if the merchant provider setting already exists
        var merchantProviderSettingExists = await merchantProviderSettingRepository.GetByMerchantIdAsync(request.MerchantId);
        if (merchantProviderSettingExists != null)
        {
            throw new MerchantProviderAlreadyConfigureException(request.MerchantId);
        }

        // Create a new merchant provider setting
        var merchantProviderSetting = MerchantProviderSetting.Create(
            merchantId: request.MerchantId,
            providerType: request.Provider,
            metaData: request.Metadata
        );

        // Save the merchant provider setting
        var merchantProviderCreated = await merchantProviderSettingRepository.CreateAsync(merchantProviderSetting);

        // Save changes
        await merchantProviderSettingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        // Return the id of the created merchant provider setting
        return merchantProviderCreated.Id;
    }
}