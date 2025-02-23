

namespace SaBooBo.OrderService.Apis;

public static class OrderApi
{
    public static RouteGroupBuilder MapOrderApi(this IEndpointRouteBuilder builder)
    {
        var apiOrder = builder.MapGroup("api/v1/orders");

        

        return apiOrder;
    }


}


