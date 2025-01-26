
using System.Security.Claims;
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.UserService.Application.Identity;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public Guid GetCurrentUser()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        
        if (userId is null)
        {
            throw new UnauthorizedException();
        }

        return Guid.Parse(userId.Value);
    }
}