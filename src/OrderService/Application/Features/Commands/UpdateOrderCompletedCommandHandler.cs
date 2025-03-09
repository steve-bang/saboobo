
using MediatR;
using SaBooBo.CartService.Domain.Exceptions;
using SaBooBo.OrderService.Domain.Repositories;

namespace SaBooBo.OrderService.Application.Features.Commands;

public class UpdateOrderCompletedCommandHandler(
    IOrderRepository _orderRepository
) : IRequestHandler<UpdateOrderCompletedCommand, bool>
{
    public async Task<bool> Handle(UpdateOrderCompletedCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
        {
            throw new OrderNotFoundException(request.OrderId);
        }

        order.UpdateOrderCompleted();

        _orderRepository.Update(order, cancellationToken);

        await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}
