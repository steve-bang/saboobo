

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


        foreach (var item in request.Items)
        {
            cart.AddItem(
                productId: item.ProductId,
                productName: item.ProductName,
                productImage: item.ProductImage,
                price: item.Price,
                quantity: item.Quantity,
                notes: item.Notes
            );
        }

        _cartRepository.UpdateCartAsync(cart);

        await _cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return cart;
    }
}