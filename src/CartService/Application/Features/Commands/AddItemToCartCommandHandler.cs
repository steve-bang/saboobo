

namespace SaBooBo.CartService.Application.Features.Commands;

public class AddItemToCartCommandHandler(
    ICartRepository _cartRepository
) : IRequestHandler<AddItemToCartCommand, Cart>
{

    public async Task<Cart> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetCartByIdAsync(request.CartId);
        if (cart == null)
        {
            throw new CartNotFoundException(request.CartId);
        }


        for (int i = 0; i < request.Items.Length; i++)
        {
            cart.AddItem(
                productId: request.Items[i].ProductId,
                productName: request.Items[i].ProductName,
                price: request.Items[i].Price,
                quantity: request.Items[i].Quantity,
                notes: request.Items[i].Notes
            );
        }

        _cartRepository.UpdateCartAsync(cart);

        await _cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return cart;
    }
}