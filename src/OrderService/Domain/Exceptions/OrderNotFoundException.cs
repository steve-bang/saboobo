

using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.OrderService.Domain.Errors;

namespace SaBooBo.CartService.Domain.Exceptions
{
    /// <summary>
    /// The order not found exception.
    /// </summary>
    public class OrderNotFoundException : BadRequestException
    {
        public OrderNotFoundException(Guid orderId) : base(
            OrderErrors.OrderNotFound,
            $"Order with id {orderId} not found.",
            "The order you are looking for does not exist. Please check the order id and try again."
        )
        {
        }
    }
}