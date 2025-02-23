
using Microsoft.AspNetCore.Mvc;
using SaBooBo.CartService.Application.Features.Commands;
using SaBooBo.CartService.Requests;
using SaBooBo.Domain.Shared.ApiResponse;
using SaBooBo.MerchantService.Apis;

namespace SaBooBo.CartService.Apis;

public static class CartApi
{
    public static RouteGroupBuilder MapCartApi(this IEndpointRouteBuilder builder)
    {
        var apiCart = builder.MapGroup("api/v1/carts");

        // POST api/v1/carts
        // Create a new cart
        apiCart.MapPost("", CreateCart);

        // POST api/v1/carts/{cartId}/items
        // Add an item to the cart
        apiCart.MapPost("{cartId}/items", AddItemToCart);

        // PUT api/v1/carts/{cartId}/items
        // Update an item in the cart
        apiCart.MapPut("{cartId}/items", UpdateItemInCart);

        // DELETE api/v1/carts/{cartId}/items
        // Delete an item from the cart
        apiCart.MapDelete("{cartId}/items", DeleteItemFromCart);

        // POST api/v1/carts/{cartId}/place-order
        // Place an order for the cart
        apiCart.MapPost("{cartId}/place-order", PlaceOrderCart).RequireAuthorization();

        return apiCart;
    }

    public static async Task<ApiResponseSuccess<Cart>> CreateCart(
        [AsParameters] ProviderService service
    )
    {
        var command = new CreateCartCommand();

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<Cart>.BuildCreated(result);
    }

    public static async Task<ApiResponseSuccess<Cart>> AddItemToCart(
        Guid cartId,
        [AsParameters] ProviderService service,
        [FromBody] ItemToCartCommandRequest[] items
    )
    {

        var result = await service.Mediator.Send(
            new AddItemToCartCommand(
                cartId,
                items.Select(x => new CartItemsCommandRequest
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Notes = x.Notes
                }).ToArray()
            )
        );

        return ApiResponseSuccess<Cart>.BuildCreated(result);
    }

    public static async Task<ApiResponseSuccess<Cart>> UpdateItemInCart(
        Guid cartId,
        [AsParameters] ProviderService service,
        [FromBody] ItemToCartCommandRequest[] items
    )
    {
        var result = await service.Mediator.Send(
            new UpdateItemCartCommand(
                cartId,
                items.Select(x => new CartItemsCommandRequest
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Notes = x.Notes
                }).ToArray()
            )
        );

        return ApiResponseSuccess<Cart>.BuildCreated(result);
    }

    public static async Task<ApiResponseSuccess<Cart>> DeleteItemFromCart(
        Guid cartId,
        [AsParameters] ProviderService service,
        [FromBody] ItemCartDeleteRequest payloadRequest
    )
    {
        var result = await service.Mediator.Send(
            new DeleteItemCartCommand(
                cartId,
                payloadRequest.CartItemIds
            )
        );

        return ApiResponseSuccess<Cart>.BuildCreated(result);
    }

    public static async Task<ApiResponseSuccess<bool>> PlaceOrderCart(
        Guid cartId,
        [AsParameters] ProviderService service,
        [FromBody] PlaceOrderCartRequest payloadRequest
    )
    {
        var result = await service.Mediator.Send(
            new PlaceOrderCartCommand(
                cartId,
                payloadRequest.ShippingAddress,
                payloadRequest.PaymentMethod,
                payloadRequest.Note,
                payloadRequest.EstimatedDeliveryDateFrom,
                payloadRequest.EstimatedDeliveryDateTo
            )
        );

        return ApiResponseSuccess<bool>.BuildSuccess(result);
    }

}


