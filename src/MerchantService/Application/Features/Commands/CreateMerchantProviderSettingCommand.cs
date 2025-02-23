
namespace MerchantService.Application.Features.Commands;

public record CreateMerchantProviderSettingCommand(
    Guid MerchantId, 
    MerchantProviderType Provider,
    string Metadata
) : IRequest<Guid>;