
using System.Text.Json;
using RabbitMqService.Constants;
using RabbitMqService.Producers;

namespace SaBooBo.CartService.Application.Features.Commands;

public class PlaceOrderCartCommandHandler(
    ICartRepository _cartRepository,
    IRabbitMqProducer _rabbitMqProducer
) : IRequestHandler<PlaceOrderCartCommand, bool>
{
    public async Task<bool> Handle(PlaceOrderCartCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetCartByIdAsync(request.CartId);

        if (cart == null)
        {
            throw new CartNotFoundException(request.CartId);
        }

        // Update cart status
        _ = ProducerMessage(request.MerchantId, cart, request);


        await _cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }

    public async Task ProducerMessage(Guid merchantId, Cart cart, PlaceOrderCartCommand request)
    {
        try
        {
            Console.WriteLine("Sending message to RabbitMQ");

            // Send message to RabbitMQ
            await _rabbitMqProducer.PublishAsync(
                exchange: string.Empty,
                routingKey: RouteKeys.CartPlaceOrder,    
                JsonSerializer.Serialize(new
                {
                    merchantId,
                    cart,
                    placeOrder = request
                })
            );
            
            // Log message sent to RabbitMQ
            Console.WriteLine("Message sent to RabbitMQ.");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
