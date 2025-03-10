
namespace RabbitMqService.Constants;

public class RouteKeys
{
    /// <summary>
    /// This route key is used to place an order from the cart.
    /// </summary>
    public const string CartPlaceOrder = "cart.place-order";

    /// <summary>
    /// This route key is used to change the status of an order.
    /// When the order is created, the message is sent to the RabbitMQ in OrderService
    /// And in the NotificationService, the message is consumed to handle changing the status of the order.
    /// </summary>
    public const string OrderChangeStatus = "order.change-status";

    /// <summary>
    /// This route key is used when the order is completed.
    /// When the order is created, the message is sent to the RabbitMQ in OrderService
    /// And in the NotificationService, the message is consumed to handle the completed order.
    /// </summary>
    public const string OrderCompleted = "order.completed";

    /// <summary>
    /// This route key is used to authenticate Zalo.
    /// When the zalo oauth callback is received, the message is sent to the RabbitMQ in WebhookService
    /// And in the NotificationService, the message is consumed to authenticate the user.
    /// </summary>
    public const string ZaloOAuthCallback = "zalo_oauth_callback";
}