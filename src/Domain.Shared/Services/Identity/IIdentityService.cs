
namespace SaBooBo.Domain.Shared.Services.Identity;

public interface IIdentityService
{
    /// <summary>
    /// Get current user id from request.
    /// </summary>
    /// <returns>Returns current user id.</returns>
    Guid GetCurrentUser();
}