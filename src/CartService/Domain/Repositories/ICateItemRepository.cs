
using SaBooBo.CartService.Domain.AggregatesModel;
using SaBooBo.Domain.Shared;

namespace SaBooBo.CartService.Domain.Repositories;

public interface ICartItemRepository : IRepository
{
    Task<CartItem> GetCartItemAsync(Guid cartItemId);

    Task AddCartItemAsync(CartItem cartItem);

    Task UpdateCartItemAsync(CartItem cartItem);

    Task DeleteCartItemAsync(Guid cartItemId);
    
}