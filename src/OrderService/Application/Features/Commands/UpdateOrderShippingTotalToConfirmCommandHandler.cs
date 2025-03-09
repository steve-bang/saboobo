
using MediatR;
using SaBooBo.CartService.Domain.Exceptions;
using SaBooBo.OrderService.Domain.Repositories;

namespace SaBooBo.OrderService.Application.Features.Commands;

public class UpdateOrderShippingTotalToConfirmCommandHandler(
    IOrderRepository _orderRepository
) : IRequestHandler<UpdateOrderShippingTotalToConfirmCommand, bool>
{
    public async Task<bool> Handle(UpdateOrderShippingTotalToConfirmCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken) ?? throw new OrderNotFoundException(request.OrderId);
        
        order.UpdateOrderConfirmed(request.ShippingTotal, request.IsFreeShipping);

        _orderRepository.Update(order, cancellationToken);

        await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}