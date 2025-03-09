
using MediatR;
using SaBooBo.CartService.Domain.Exceptions;
using SaBooBo.OrderService.Domain.Repositories;

namespace SaBooBo.OrderService.Application.Features.Commands;

public class UpdateOrderShippingStatusCommandHandler(
    IOrderRepository _orderRepository
) : IRequestHandler<UpdateOrderShippingStatusCommand, bool>
{
    public async Task<bool> Handle(UpdateOrderShippingStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
        {
            throw new OrderNotFoundException(request.OrderId);
        }

        order.UpdateOrderShipping();

        _orderRepository.Update(order, cancellationToken);

        await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}