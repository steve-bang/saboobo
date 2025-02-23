
namespace MerchantService.Application.Features.Commands;

public record UpdateMerchantProviderSettingByIdCommand(
    Guid MerchantId,
    Guid MerchantProviderSettingId,
    MerchantProviderType ProviderType,
    string Metadata
) : IRequest<Guid>;