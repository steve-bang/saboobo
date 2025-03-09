
using MediatR;
using SaBooBo.CartService.Domain.Exceptions;
using SaBooBo.OrderService.Domain.Repositories;

namespace SaBooBo.OrderService.Application.Features.Commands;

public class UpdateOrderCancelCommandHandler(
    IOrderRepository _orderRepository
) : IRequestHandler<UpdateOrderCancelCommand, bool>
{
    public async Task<bool> Handle(UpdateOrderCancelCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
        {
            throw new OrderNotFoundException(request.OrderId);
        }

        order.UpdateOrderCancelled();

        _orderRepository.Update(order, cancellationToken);

        await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}
