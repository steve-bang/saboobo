
using Microsoft.EntityFrameworkCore;
using SaBooBo.CartService.Domain.AggregatesModel;
using SaBooBo.CartService.Domain.Repositories;

namespace SaBooBo.CartService.Infrastructure.Repository;

public class CartRepository(
    CartAppContext _context
) : ICartRepository
{
    public IUnitOfWork UnitOfWork => _context;

    public async Task<Cart> AddCartAsync(Cart cart)
    {
        var cartEntity = await _context.Carts.AddAsync(cart);

        return cartEntity.Entity;
    }

    public bool DeleteById(Cart cart)
    {
        return _context.Carts.Remove(cart).State == Microsoft.EntityFrameworkCore.EntityState.Deleted;
    }

    public async Task<bool> DeleteByIdAsync(Guid cartId)
    {
        var cart = await GetCartByIdAsync(cartId);

        if (cart == null)
        {
            return false;
        }

        return DeleteById(cart);
    }

    public Task<Cart?> GetByCustomerIdAsync(Guid customerId)
    {
        return _context.Carts.FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }

    public async Task<Cart?> GetCartByIdAsync(Guid cartId)
    {
        return await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);
    }

    public void UpdateCartAsync(Cart cart)
    {
        _context.Carts.Update(cart);
    }
}