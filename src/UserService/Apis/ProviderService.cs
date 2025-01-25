
using SaBooBo.UserService.Application.Identity;

namespace SaBooBo.UserService.Apis;

public class UserServices(
    IMediator mediator,
    ILogger<UserServices> logger,
    IIdentityService identityService
)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<UserServices> Logger { get; } = logger;
    public IIdentityService IdentityService { get; } = identityService;
}