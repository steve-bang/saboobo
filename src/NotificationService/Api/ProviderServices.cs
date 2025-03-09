
using MediatR;

namespace SaBooBo.NotificationService.Apis;

public class ProviderServices(
    IMediator mediator,
    ILogger<ProviderServices> logger
)
{
    public IMediator Mediator { get; set; } = mediator;
    
    public ILogger<ProviderServices> Logger { get; } = logger;

}