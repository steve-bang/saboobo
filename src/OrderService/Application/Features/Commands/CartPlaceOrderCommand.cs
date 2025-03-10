
using MediatR;

namespace SaBooBo.OrderService.Application.Features.Commands;

public record CartPlaceOrderCommand(
    CartPlaceOrder Request
) : IRequest<bool>;

public record CartPlaceOrder(
    Guid MerchantId,
    string ZaloOrderId,
    Cart Cart,
    PlaceOrder PlaceOrder
);


public record Cart (
    Guid Id,
    Guid CustomerId,
    decimal TotalPrice,
    List<CartItem> Items,
    DateTime CreatedAt,
    DateTime UpdatedAt
    
);

public record CartItem (
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice,
    string Notes
);

public record PlaceOrder (
    Guid CartId,
    ShippingAddress ShippingAddress,
    string? PaymentMethod,
    string? Note,
    DateTime EstimatedDeliveryDateFrom,
    DateTime EstimatedDeliveryDateTo
);

public record ShippingAddress (
    string PhoneNumber,
    string Name,
    string AddressDetail
);


