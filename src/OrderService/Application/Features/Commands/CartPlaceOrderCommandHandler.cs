
using MediatR;
using SaBooBo.OrderService.Domain.AggregatesModel;
using SaBooBo.OrderService.Domain.Repositories;

namespace SaBooBo.OrderService.Application.Features.Commands;

public class CartPlaceOrderCommandHandler(
    IOrderRepository _orderRepository
) : IRequestHandler<CartPlaceOrderCommand, bool>
{
    public async Task<bool> Handle(CartPlaceOrderCommand request, CancellationToken cancellationToken)
    {

        // Create a new order
        var order = Order.Create(
            merchantId: request.Request.MerchantId,
            customerId: request.Request.Cart.CustomerId,
            zaloOrderId: request.Request.ZaloOrderId,
            code: Guid.NewGuid().ToString(),
            paymentMethod: request.Request.PlaceOrder.PaymentMethod ?? string.Empty,
            ipAddress: string.Empty,
            notes: request.Request.PlaceOrder.Note,
            estimatedTimeDeliveryFrom: request.Request.PlaceOrder.EstimatedDeliveryDateFrom,
            estimatedTimeDeliveryTo: request.Request.PlaceOrder.EstimatedDeliveryDateTo
        );

        // Add items to the order
        foreach (var item in request.Request.Cart.Items)
        {
            order.AddItem(
                productId: item.ProductId,
                productName: item.ProductName,
                unitPrice: item.UnitPrice,
                quantity: item.Quantity,
                imageUrl: string.Empty,
                notes: item.Notes
            );
        }

        // Add shipping address to the order
        order.AddShippingAddress(
            name: request.Request.PlaceOrder.ShippingAddress.Name,
            phoneNumber: request.Request.PlaceOrder.ShippingAddress.PhoneNumber,
            addressDetail: request.Request.PlaceOrder.ShippingAddress.AddressDetail
        );

        await _orderRepository.CreateAsync(order);

        // Save the changes to the database
        await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}


