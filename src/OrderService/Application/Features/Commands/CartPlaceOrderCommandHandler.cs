
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

        var order = Order.Create(
            customerId: request.Request.Cart.CustomerId,
            code: Guid.NewGuid().ToString(),
            paymentMethod: request.Request.PlaceOrder.PaymentMethod ?? string.Empty,
            ipAddress: string.Empty,
            notes: request.Request.PlaceOrder.Note,
            estimatedTimeDeliveryFrom: request.Request.PlaceOrder.EstimatedDeliveryDateFrom,
            estimatedTimeDeliveryTo: request.Request.PlaceOrder.EstimatedDeliveryDateTo
        );

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

        await _orderRepository.CreateAsync(order);

        await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}


