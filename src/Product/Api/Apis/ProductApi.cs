
using Microsoft.AspNetCore.Mvc;
using SaBooBo.Product.Application.Features.Commands;

namespace SaBooBo.Product.Api;

public static class ProductApi
{
    public static RouteGroupBuilder MapProductApi(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var api = endpointRouteBuilder.MapGroup("api/v1/products");

        api.MapPost("", CreateProduct);

        return api;
    }

    public static async Task<Guid> CreateProduct(
        [FromBody] ProductCreateCommand productCreateCommand, 
        [AsParameters] ProductServices productServices
    )
    {
        var result = await productServices.Mediator.Send(productCreateCommand);

        return result;
    }
}