
using SaBooBo.CustomerService.Application.Features.Queries;
using SaBooBo.Domain.Shared.ApiResponse;

namespace SaBooBo.CustomerService.WebApi;


public static class MerchantApi
{
    public static RouteGroupBuilder MapMerchantApi(this IEndpointRouteBuilder builder)
    {
        var apiCustomer = builder.MapGroup("api/v1/merchants");

        // List customers by merchant id
        // GET api/v1/merchant/{id}/customers
        apiCustomer.MapGet("{id}/customers", ListCustomerByMerchantId);


        return apiCustomer;
    }

    public static async Task<ApiResponseSuccess<List<Customer>>> ListCustomerByMerchantId(
        Guid id,
        [AsParameters] CustomerServices service
    )
    {
        ListCustomersByMerchantIdQuery query = new(id);

        var result = await service.Mediator.Send(query);

        return ApiResponseSuccess<List<Customer>>.BuildCreated(result);
    }

}