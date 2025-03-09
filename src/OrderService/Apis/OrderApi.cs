

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SaBooBo.Domain.Shared;
using SaBooBo.Domain.Shared.ApiResponse;
using SaBooBo.OrderService.Application.Features.Commands;

namespace SaBooBo.OrderService.Apis;

public static class OrderApi
{
    public static RouteGroupBuilder MapOrderApi(this IEndpointRouteBuilder builder)
    {
        var apiOrder = builder.MapGroup("api/v1/orders");

        // POST /api/v1/orders/{orderId}/confirm
        // Update the order status to confirmed
        apiOrder.MapPost("/{orderId}/confirm", UpdateOrderConfirmedToShipping);

        // POST /api/v1/orders/{orderId}/shipping
        // Update the order status to shipping
        apiOrder.MapPost("/{orderId}/shipping", UpdateOrderShipping);

        // POST /api/v1/orders/{orderId}/completed
        // Update the order status to completed
        apiOrder.MapPost("/{orderId}/completed", UpdateOrderCompleted);

        // POST /api/v1/orders/{orderId}/cancel
        // Update the order status to cancelled
        apiOrder.MapPost("/{orderId}/cancel", UpdateOrderCancel);

        return apiOrder;
    }

    public static async Task<IResult> UpdateOrderConfirmedToShipping(
        [FromHeader(Name = Constants.Headers.MerchantId)] Guid merchantId,
        Guid orderId,
        [FromQuery][Required] decimal shippingTotal,
        [FromQuery] bool? isFreeShipping,
        [AsParameters] ProviderService providerService
    )
    {
        var command = new UpdateOrderShippingTotalToConfirmCommand(
            merchantId,
            orderId,
            shippingTotal,
            isFreeShipping ?? false
        );
        var result = await providerService.Mediator.Send(command);

        return ApiResponseSuccess<bool>.BuildSuccessResult(result);
    }

    public static async Task<IResult> UpdateOrderShipping(
        [FromHeader(Name = Constants.Headers.MerchantId)] Guid merchantId,
        Guid orderId,
        [AsParameters] ProviderService providerService
    )
    {
        var command = new UpdateOrderShippingStatusCommand(merchantId, orderId);
        var result = await providerService.Mediator.Send(command);

        return ApiResponseSuccess<bool>.BuildSuccessResult(result);
    }

    public static async Task<IResult> UpdateOrderCompleted(
        [FromHeader(Name = Constants.Headers.MerchantId)] Guid merchantId,
        Guid orderId,
        [AsParameters] ProviderService providerService
    )
    {
        var command = new UpdateOrderCompletedCommand(merchantId, orderId);
        var result = await providerService.Mediator.Send(command);

        return ApiResponseSuccess<bool>.BuildSuccessResult(result);
    }

    public static async Task<IResult> UpdateOrderCancel(
        [FromHeader(Name = Constants.Headers.MerchantId)] Guid merchantId,
        Guid orderId,
        [AsParameters] ProviderService providerService
    )
    {
        var command = new UpdateOrderCancelCommand(merchantId, orderId);
        var result = await providerService.Mediator.Send(command);

        return ApiResponseSuccess<bool>.BuildSuccessResult(result);
    }



}


