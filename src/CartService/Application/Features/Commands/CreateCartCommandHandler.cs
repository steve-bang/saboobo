
using SaBooBo.Domain.Shared.Services.Identity;

namespace SaBooBo.CartService.Application.Features.Commands;

public class CreateCartCommandHandler (
    ICartRepository cartRepository,
    IIdentityService identityService
) : IRequestHandler<CreateCartCommand, Cart>
{
    public async Task<Cart> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var customerId = identityService.GetCurrentUser();

        // Check if the cart already exists
        var cart = await cartRepository.GetByCustomerIdAsync(customerId);
            
        // If the cart already exists, return it
        if (cart != null)
        {
            return cart;
        }

        // If the cart does not exist, create a new cart
        cart = new Cart(customerId);

        await cartRepository.AddCartAsync(cart);

        await cartRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return cart;
    }
}