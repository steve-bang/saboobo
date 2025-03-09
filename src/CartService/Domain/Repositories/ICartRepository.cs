

namespace SaBooBo.CartService.Domain.Repositories;

public interface ICartRepository : IRepository
{   
    /// <summary>
    /// Get cart by customer id
    /// </summary>
    /// <param name="customerId">The customer id</param>
    /// <returns></returns>
    Task<Cart?> GetByCustomerIdAsync(Guid customerId);

    /// <summary>
    /// Get cart by cart id
    /// </summary>
    /// <param name="cartId"></param>
    /// <returns></returns>
    Task<Cart?> GetCartByIdAsync(Guid cartId);

    /// <summary>
    /// Add cart 
    /// </summary>
    /// <param name="cart"></param>
    /// <returns></returns>
    Task<Cart> AddCartAsync(Cart cart);

    /// <summary>
    /// Update cart
    /// </summary>
    /// <param name="cart"></param>
    void UpdateCartAsync(Cart cart);

    /// <summary>
    /// Delete cart by cart id
    /// </summary>
    /// <param name="cart"></param>
    /// <returns></returns>
    bool DeleteById(Cart cart);

    /// <summary>
    /// Delete cart by cart id
    /// </summary>
    /// <param name="cartId"></param>
    /// <returns></returns>
    Task<bool> DeleteByIdAsync(Guid cartId);

}