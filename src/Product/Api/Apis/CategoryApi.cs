
using Microsoft.AspNetCore.Mvc;
using SaBooBo.Domain.Shared.ApiResponse;
using SaBooBo.Product.Application.Features.Commands;
using SaBooBo.Product.Application.Features.Queries;
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Api;

public static class CategoryApi
{
    public static RouteGroupBuilder MapCategoryApi(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var api = endpointRouteBuilder.MapGroup("api/v1/merchants/{merchantId}/categories");

        // Create a new category
        // POST api/v1/products
        api.MapPost("", CreateCategory);

        // Get category by id
        // GET api/v1/products/:id
        api.MapGet("{id}", GetCategoryById);

        // Update product by id
        // PUT api/v1/products/:id
        api.MapPut("{id}", UpdateCategory);

        // Delete category by id
        // DELETE api/v1/products/:id
        api.MapDelete("{id}", DeleteCategoryById);

        // Get all categories
        // GET api/v1/products
        api.MapGet("", ListCategory);

        return api;
    }

    public static async Task<IResult> CreateCategory(
    Guid merchantId,
    [FromBody] CategoryCommandRequest payload,
    [AsParameters] ProviderServices providerServices
    )
    {
        CreateCategoryCommand command = new(
            MerchantId: merchantId,
            Name: payload.Name,
            Code: payload.Code,
            Description: payload.Description,
            IconUrl: payload.IconUrl
        );

        var result = await providerServices.Mediator.Send(command);

        return Results.Created("api/v1/merchants/{merchantId}/categories", ApiResponseSuccess<Guid>.BuildCreated(result));
    }

    public static async Task<ApiResponseSuccess<Category>> GetCategoryById(
        Guid merchantId,
        Guid id,
        [AsParameters] ProviderServices providerServices
    )
    {
        GetCategoryByIdQuery query = new(id);

        var result = await providerServices.Mediator.Send(query);

        return ApiResponseSuccess<Category>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<bool>> DeleteCategoryById(
        Guid merchantId,
        Guid id,
        [AsParameters] ProviderServices providerServices
    )
    {
        DeleteCategoryByIdCommand command = new(id);

        var result = await providerServices.Mediator.Send(command);

        return ApiResponseSuccess<bool>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<List<Category>>> ListCategory(
        Guid merchantId,
        [AsParameters] ProviderServices providerServices
    )
    {
        ListCategoryQuery query = new(merchantId);

        var result = await providerServices.Mediator.Send(query);

        return ApiResponseSuccess<List<Category>>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<Category>> UpdateCategory(
        Guid merchantId,
        Guid id,
        [FromBody] CategoryCommandRequest payload,
        [AsParameters] ProviderServices providerServices
    )
    {
        UpdateCategoryCommand command = new(
            Id: id,
            Name: payload.Name,
            Code: payload.Code,
            Description: payload.Description,
            IconUrl: payload.IconUrl
        );

        var result = await providerServices.Mediator.Send(command);

        return ApiResponseSuccess<Category>.BuildSuccess(result);
    }

}

public record CategoryCommandRequest(
    string Name,
    string? Code,
    string? Description,
    string? IconUrl
);