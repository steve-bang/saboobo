
using SaBooBo.CartService.Domain.Errors;
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.CartService.Domain.Exceptions
{
    public class CartNotFoundException : BadRequestException
    {
        public CartNotFoundException(Guid cartId) : base(
            CartErrors.CartNotFound,
            $"Cart with id {cartId} not found.",
            "The cart you are looking for does not exist. Please check the cart id and try again."
        )
        {
        }
    }
}