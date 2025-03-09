
namespace RabbitMqService.Constants;

public class RouteKeys
{   
    /// <summary>
    /// This route key is used to place an order from the cart.
    /// </summary>
    public const string CartPlaceOrder = "cart.place-order";

    /// <summary>
    /// This route key is used to create an order and set status to pending.
    /// When the order is created, the message is sent to the RabbitMQ in OrderService
    /// And in the NotificationService, the message is consumed to create an order and set status to pending.
    /// </summary>
    public const string OrderChangeStatus = "order.change-status";

    /// <summary>
    /// This route key is used to authenticate Zalo.
    /// When the zalo oauth callback is received, the message is sent to the RabbitMQ in WebhookService
    /// And in the NotificationService, the message is consumed to authenticate the user.
    /// </summary>
    public const string ZaloOAuthCallback = "zalo_oauth_callback";
}