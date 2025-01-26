using SaBooBo.UserService.Application.Identity;
namespace SaBooBo.MerchantService.Apis;

public class MerchantServices(
    IMediator mediator,
    ILogger<MerchantServices> logger,
    IIdentityService identityService
)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<MerchantServices> Logger { get; } = logger;

    public IIdentityService IdentityService { get; } = identityService;
}