
using SaBooBo.Domain.Common;
using SaBooBo.NotificationService.Models;

namespace SaBooBo.NotificationService.Domain.Services;

public interface ISmsService 
{
    /// <summary>
    /// Send Zalo transaction notification
    /// </summary>
    /// <param name="request">The request model</param>
    /// <returns></returns>
    Task SendZaloTransactionNotificationAsync(Guid merchantId, ZaloMessageTemplate request);

}