
namespace SaBooBo.CartService.Application.Models;

public class CartItemsCommandRequest 
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? Notes { get; set; }
}