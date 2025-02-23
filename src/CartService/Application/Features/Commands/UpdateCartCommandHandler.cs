
namespace SaBooBo.CartService.Application.Features.Commands;


public class UpdateCartCommandHandler(
    ICartRepository cartRepository
) : IRequestHandler<UpdateCartCommand, Cart>
{

    public async Task<Cart> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await cartRepository.GetCartByIdAsync(request.CartId);

        if (cart == null)
        {
            throw new CartNotFoundException(request.CartId);
        }

        cart.UpdateItem(request.ProductId, request.Quantity, request.Notes);

        cartRepository.UpdateCartAsync(cart);

        await cartRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return cart;
    }
}