
using System.Security.Claims;

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
            throw new InvalidOperationException("User is not authenticated.");
        }

        return Guid.Parse(userId.Value);
    }
}