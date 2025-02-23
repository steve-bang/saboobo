
using MerchantService.Application.Features.Commands;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SaBooBo.Domain.Shared.ApiResponse;
using SaBooBo.MerchantService.Apis.Requests;
using SaBooBo.MerchantService.Application.Features.Queries;

namespace SaBooBo.MerchantService.Apis;

public static class MerchantProviderSettingApi
{
    public static RouteGroupBuilder MapMerchantProviderSettingApi(this IEndpointRouteBuilder builder)
    {
        var apiMerchant = builder.MapGroup("api/v1/merchants/{merchantId}/provider-settings");


        // POST /api/v1/merchants/{merchantId}/provider-settings
        // Create merchant
        apiMerchant.MapPost("", CreateMerchantProviderSetting);

        // PUT /api/v1/merchants/{merchantId}/provider-settings/{providerSettingId}
        // Update merchant
        apiMerchant.MapPut("{providerSettingId}", UpdateMerchantProviderSetting);

        // GET /api/v1/merchants/{merchantId}/provider-settings
        // Get provider setting by merchant id
        apiMerchant.MapGet("", GetByMerchantId);

        return apiMerchant;
    }


    public static async Task<ApiResponseSuccess<Guid>> CreateMerchantProviderSetting(
        Guid merchantId,
        [FromBody] MerchantProviderSettingCommandRequest createMerchantRequest,
        [AsParameters] MerchantServices service
    )
    {
        var command = new CreateMerchantProviderSettingCommand(
            merchantId,
            createMerchantRequest.ProviderType,
            JsonConvert.SerializeObject(createMerchantRequest.Metadata)
        );

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<Guid>.BuildCreated(result);
    }

    public static async Task<ApiResponseSuccess<Guid>> UpdateMerchantProviderSetting(
        Guid merchantId,
        Guid providerSettingId,
        [FromBody] MerchantProviderSettingCommandRequest createMerchantRequest,
        [AsParameters] MerchantServices service
    )
    {
        var command = new UpdateMerchantProviderSettingByIdCommand(
            merchantId,
            providerSettingId,
            createMerchantRequest.ProviderType,
            JsonConvert.SerializeObject(createMerchantRequest.Metadata)
        );

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<Guid>.BuildCreated(result);
    }

    public static async Task<ApiResponseSuccess<MerchantProviderSetting>> GetByMerchantId(
        Guid merchantId,
        [AsParameters] MerchantServices service
    )
    {
        var query = new GetMerchantProviderSettingByMerchantIdQuery(merchantId);

        var result = await service.Mediator.Send(query);

        return ApiResponseSuccess<MerchantProviderSetting>.BuildSuccess(result);
    }

}

