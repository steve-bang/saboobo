
using Microsoft.AspNetCore.Mvc;
using SaBooBo.Domain.Shared.ApiResponse;
using SaBooBo.Product.Application.Features.Commands;
using SaBooBo.Product.Application.Features.Queries;
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Api;

public static class ProductApi
{
    public static RouteGroupBuilder MapProductApi(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var api = endpointRouteBuilder.MapGroup("api/v1/products");

        // Create a new product
        // POST api/v1/products
        api.MapPost("", CreateProduct);

        // Get product by id
        // GET api/v1/products/:id
        api.MapGet("{id}", GetProductById);

        // Update product by id
        // PUT api/v1/products/:id
        api.MapPut("{id}", UpdateProductById);

        // Delete product by id
        // DELETE api/v1/products/:id
        api.MapDelete("{id}", DeleteProductById);

        // Get all products
        // GET api/v1/products
        api.MapGet("", GetAllProducts);

        return api;
    }

    public static async Task<IResult> CreateProduct(
        [FromBody] CreateProductCommand productCreateCommand,
        [AsParameters] ProviderServices productServices
    )
    {
        Guid result = await productServices.Mediator.Send(productCreateCommand);

        return Results.Created("api/v1/products", ApiResponseSuccess<Guid>.BuildCreated(result));
    }

    public static async Task<ApiResponseSuccess<Domain.AggregatesModel.Product>> GetProductById(
        Guid id,
        [AsParameters] ProviderServices productServices
    )
    {
        var productGetByIdQuery = new GetProductByIdQuery(id);

        var result = await productServices.Mediator.Send(productGetByIdQuery);

        return ApiResponseSuccess<Domain.AggregatesModel.Product>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<bool>> DeleteProductById(
        Guid id,
        [AsParameters] ProviderServices productServices
    )
    {
        var productGetByIdQuery = new DeleteProductCommand(id);

        var result = await productServices.Mediator.Send(productGetByIdQuery);

        return ApiResponseSuccess<bool>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<Domain.AggregatesModel.Product>> UpdateProductById(
        Guid id,
        [FromBody] UpdateProductRequest request,
        [AsParameters] ProviderServices productServices
    )
    {
        UpdateProductCommand updateProductCommand = new UpdateProductCommand(
            Id: id,
            Name: request.Name,
            Sku: request.Sku,
            Price: request.Price,
            Description: request.Description,
            UrlImage: request.UrlImage,
            Toppings: request.Toppings
        );

        var result = await productServices.Mediator.Send(updateProductCommand);

        return ApiResponseSuccess<Domain.AggregatesModel.Product>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<List<Domain.AggregatesModel.Product>>> GetAllProducts(
    [AsParameters] ProviderServices productServices
    )
    {
        ListProductQuery listProductQuery = new();

        var result = await productServices.Mediator.Send(listProductQuery);

        return ApiResponseSuccess<List<Domain.AggregatesModel.Product>>.BuildSuccess(result);
    }
}

public class UpdateProductRequest
{
    public string Name { get; set; } = null!;

    public string? Sku { get; set; }

    public long Price { get; set; }

    public string? Description { get; set; }

    public string? UrlImage { get; set; }

    public ToppingUpdateRequest[]? Toppings { get; set; }
}