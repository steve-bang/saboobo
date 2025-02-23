
namespace SaBooBo.CartService.Application.Features.Commands;

public class DeleteItemCartCommandHandler(
    ICartRepository _cartRepository
) : IRequestHandler<DeleteItemCartCommand, Cart>
{

    public async Task<Cart> Handle(DeleteItemCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartByIdAsync(request.CartId);
        if (cart == null)
        {
            throw new CartNotFoundException(request.CartId);
        }

        foreach (var cartItemId in request.CartItemIds)
        {
            cart.RemoveItem(cartItemId);
        }

        _cartRepository.UpdateCartAsync(cart);

        await _cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return cart;
    }
}