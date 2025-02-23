
using SaBooBo.CartService.Application.Features.Commands;

namespace SaBooBo.CartService.Requests;

public class PlaceOrderCartRequest
{    
    public string? PaymentMethod { get; set; }

    public ShippingAddress ShippingAddress { get; set; } = null!;

    public string? Note { get; set; }

    public DateTime EstimatedDeliveryDateFrom { get; set; }

    public DateTime EstimatedDeliveryDateTo { get; set; }
}

