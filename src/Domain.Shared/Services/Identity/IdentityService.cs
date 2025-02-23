
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.Domain.Shared.Services.Identity;

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