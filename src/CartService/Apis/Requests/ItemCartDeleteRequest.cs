
namespace SaBooBo.CartService.Requests;

public class ItemCartDeleteRequest
{
    public Guid[] CartItemIds { get; set; } = null!;
    
}