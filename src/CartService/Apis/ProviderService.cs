
namespace SaBooBo.MerchantService.Apis;

public class ProviderService(
    IMediator mediator,
    ILogger<ProviderService> logger
)
{
    public IMediator Mediator { get; set; } = mediator;
    
    public ILogger<ProviderService> Logger { get; } = logger;

}