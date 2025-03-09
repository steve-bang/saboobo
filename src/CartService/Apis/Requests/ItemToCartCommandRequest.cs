
namespace SaBooBo.CartService.Requests;

public class ItemToCartCommandRequest
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductImage { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? Notes { get; set; }
}