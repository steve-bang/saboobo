
namespace SaBooBo.OrderService.Domain.AggregatesModel
{
    /// <summary>
    /// Possible order status
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// The order is pending.
        /// When the order is created, The order has been created, but hasn't been processed yet.
        /// Example: The customer has just placed the order, but it's waiting for restaurant confirmation.
        /// </summary>
        Pending,

        /// <summary>
        /// The order is confirmed.
        /// The restaurant has accepted the order and is preparing it.
        /// Example: The restaurant has confirmed the order and is preparing the food.
        /// </summary>
        Confirmed,


        /// <summary>
        /// The order is payment pending.
        /// Example: The customer wants to pay by cash on delivery.
        /// </summary>
        PaymentPending,

        /// <summary>
        /// The payment is completed.
        /// When the payment is completed, it is in this status.
        /// </summary>
        PaymentCompleted,

        /// <summary>
        /// The order is shipping.
        /// When the order is being processed, it is in this status.
        /// </summary>
        Shipping,

        /// <summary>
        /// The order is completed.
        /// When the order is completed, it is in this status.
        /// </summary>
        Completed,

        /// <summary>
        /// The order is out for delivery.
        /// When the order is out for delivery, it is in this status.
        /// Example: The delivery person has picked up the order and is on the way to deliver it.
        /// </summary>
        OutForDelivery,

        /// <summary>
        /// The order is delivered.
        /// When the order is delivered, it is in this status.
        /// Example: The delivery person has delivered the order to the customer.
        /// </summary>
        Delivered,

        /// <summary>
        /// The order is cancelled.
        /// When the order is cancelled, it is in this status.
        /// </summary>
        Cancelled,

        /// <summary>
        /// The order is refunded.
        /// When the order is refunded, it is in this status.
        /// Example: The customer has requested a refund and the refund has been processed.
        /// </summary>
        Refunded,

        /// <summary>
        /// The order is failed.
        /// When the order is failed, it is in this status.
        /// Example: The payment has failed.
        /// </summary>
        Failed
    }
}