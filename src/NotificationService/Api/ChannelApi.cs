

using Microsoft.AspNetCore.Mvc;
using SaBooBo.Domain.Shared.ApiResponse;
using SaBooBo.NotificationService.Application.Features.Commands;
using SaBooBo.NotificationService.Application.Features.Queries;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Models.DTO;

namespace SaBooBo.NotificationService.Apis;

public static class ChannelApi
{
    public static RouteGroupBuilder MapChannelApi(this IEndpointRouteBuilder builder)
    {
        var apiChannel = builder.MapGroup("api/v1/channels");

        // GET /api/v1/channels
        // List all channels
        apiChannel.MapGet("", ListChannels);

        // POST /api/v1/channels
        // Create a new channel
        apiChannel.MapPost("", CreateChannel);

        // POST /api/v1/channels/{channelId}/configs
        // Create a new channel config
        apiChannel.MapPost("{channelId}/configs", CreateChannelConfig);

        // GET /api/v1/channels/{channelId}/configs
        // List channel configs
        apiChannel.MapGet("{channelId}/configs", ListChannelConfig);

        return apiChannel;
    }

    public static async Task<IResult> CreateChannel(
        [FromBody] Channel channel,
        [AsParameters] ProviderServices providerServices
    )
    {
        var command = new CreateChannelCommand(channel);

        var result = await providerServices.Mediator.Send(command);

        return ApiResponseSuccess<Channel>.BuildCreated(string.Empty, result);
    }

    public static async Task<ApiResponseSuccess<List<Channel>>> ListChannels(
        [AsParameters] ProviderServices providerServices
    )
    {
        var query = new ListChannelQuery();

        var result = await providerServices.Mediator.Send(query);

        return ApiResponseSuccess<List<Channel>>.BuildSuccess(result);
    }

    public static async Task<IResult> CreateChannelConfig(
        Guid channelId, // Get channel id from route
        [FromHeader(Name = "X-Merchant-Id")] Guid merchantId, // Get merchant id from header with name "X-Merchant-Id"
        [FromBody] ChannelConfigCommandRequestDto channelConfig, // Get channel config from request body
        [AsParameters] ProviderServices providerServices
    )
    {
        var command = new CreateChannelConfigCommand(
            channelId,
            merchantId,
            channelConfig
        );

        var result = await providerServices.Mediator.Send(command);

        return ApiResponseSuccess<ChannelConfig>.BuildCreated(string.Empty, result);
    }

    public static async Task<IResult> ListChannelConfig(
        Guid channelId, // Get channel id from route
        [FromHeader(Name = "X-Merchant-Id")] Guid merchantId, // Get merchant id from header with name "X-Merchant-Id"
        [AsParameters] ProviderServices providerServices
    )
    {
        var command = new GetChanneConfigByMerchantQuery(
            channelId,
            merchantId
        );

        var result = await providerServices.Mediator.Send(command);

        return ApiResponseSuccess<ChannelConfig>.BuildSuccessResult(result);
    }
}