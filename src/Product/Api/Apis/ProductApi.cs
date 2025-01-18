
using Microsoft.AspNetCore.Mvc;
using SaBooBo.Product.Application.Features.Commands;
using SaBooBo.Product.Application.Features.Queries;

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

    public static async Task<Domain.AggregatesModel.Product?> GetProductById(
        Guid id, 
        [AsParameters] ProductServices productServices
    )
    {
        var productGetByIdQuery = new GetProductByIdQuery(id);

        var result = await productServices.Mediator.Send(productGetByIdQuery);

        return result;
    }

    public static async Task<bool> DeleteProductById(
        Guid id, 
        [AsParameters] ProductServices productServices
    )
    {
        var productGetByIdQuery = new DeleteProductCommand(id);

        var result = await productServices.Mediator.Send(productGetByIdQuery);

        return result;
    }

    public static async Task<Domain.AggregatesModel.Product> UpdateProductById(
        Guid id, 
        [FromBody] UpdateProductRequest request,
        [AsParameters] ProductServices productServices
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

        return result;
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