
using SaBooBo.NotificationService.Clients;

namespace SaBooBo.Domain.Common;

public abstract class ZaloMessageTemplate
{
    public string UserZaloId { get; set; } = null!;
    public Guid OrderId { get; set; }
    public double TotalPrice { get; set; }
    public string MiniZaloAppLink { get; set; } = null!;
    public string PhoneNumberOwner { get; set; } = null!;

    public abstract object BuildMessage();

    /// <summary>
    /// Create a new Zalo message template
    /// </summary>
    public static ZaloMessageTemplate Create(
        string userId,
        Guid orderId,
        string status,
        double? shippingTotal,
        double totalPrice,
        string miniZaloAppLink,
        string phoneNumberOwner
    )
    {
        switch (status)
        {
            case OrderStatus.Pending:
                return new ZaloMessageWaitConfirmTemplate(userId, orderId, totalPrice, miniZaloAppLink, phoneNumberOwner);
            case OrderStatus.Confirmed:
                return new ZaloMessageConfirmedTemplate(userId, orderId, totalPrice, miniZaloAppLink, phoneNumberOwner, shippingTotal ?? 0);
            case OrderStatus.Shipping:
                return new ZaloMessageDeliveryTemplate(userId, orderId, totalPrice, miniZaloAppLink, phoneNumberOwner, shippingTotal ?? 0);
            case OrderStatus.Completed:
                return new ZaloMessageCompleteTemplate(userId, orderId, totalPrice, miniZaloAppLink, phoneNumberOwner, shippingTotal ?? 0);
            default:
                throw new InvalidOperationException($"Invalid order status: {status}");

        }
    }
}