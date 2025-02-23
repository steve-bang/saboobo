

namespace SaBooBo.CartService.Application.Features.Commands;

public class UpdateItemCartCommandHandler(
    ICartRepository cartRepository
) : IRequestHandler<UpdateItemCartCommand, Cart>
{
    public async Task<Cart> Handle(UpdateItemCartCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await cartRepository.GetCartByIdAsync(request.CartId);

        if (cart == null)
        {
            throw new CartNotFoundException(request.CartId);
        }

        foreach (var item in request.Items)
        {
            cart.UpdateItem(item.ProductId, item.Quantity, item.Notes);
        }

        cartRepository.UpdateCartAsync(cart);

        await cartRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return cart;
    }
}