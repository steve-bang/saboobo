
using MerchantService.Application.Features.Commands;
using MerchantService.Application.Features.Queries;
using Microsoft.AspNetCore.Mvc;
using SaBooBo.Domain.Shared.ApiResponse;
using SaBooBo.MerchantService.Apis.Requests;

namespace SaBooBo.MerchantService.Apis;

public static class MerchantApi
{
    public static RouteGroupBuilder MapMerchantApi(this IEndpointRouteBuilder builder)
    {
        var apiMerchant = builder.MapGroup("api/v1/merchants");

        // PUT /api/v1/merchants/{id}
        // Update merchant by id
        apiMerchant.MapPut("{id}", UpdateMerchant);

        // POST /api/v1/merchants
        // Create merchant
        apiMerchant.MapPost("", CreateMerchant);

        // GET /api/v1/merchants/me
        // Get merchant by user id
        apiMerchant.MapGet("", GetMerchantByUserId);

        // GET /api/v1/merchants/{id}
        // Get merchant by id
        apiMerchant.MapGet("{id}", GetMerchantById);

        return apiMerchant;
    }

    public static async Task<ApiResponseSuccess<Merchant>> UpdateMerchant(
        Guid id,
        [FromBody] MerchantCommandRequest updateMerchantRequest,
        [AsParameters] MerchantServices service
    )
    {
        var command = new UpdateMerchantCommand(
            id,
            updateMerchantRequest.Name,
            updateMerchantRequest.Description,
            updateMerchantRequest.EmailAddress,
            updateMerchantRequest.PhoneNumber,
            updateMerchantRequest.Address,
            updateMerchantRequest.LogoUrl,
            updateMerchantRequest.CoverUrl,
            updateMerchantRequest.Website,
            updateMerchantRequest.OaUrl
        );

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<Merchant>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<Merchant>> CreateMerchant(
        [FromBody] MerchantCommandRequest createMerchantRequest,
        [AsParameters] MerchantServices service
    )
    {
        var userId = service.IdentityService.GetCurrentUser();

        var command = new CreateMerchantCommand(
            userId,
            createMerchantRequest.Name,
            createMerchantRequest.Code,
            createMerchantRequest.Description,
            createMerchantRequest.EmailAddress,
            createMerchantRequest.PhoneNumber,
            createMerchantRequest.Address,
            createMerchantRequest.LogoUrl,
            createMerchantRequest.CoverUrl,
            createMerchantRequest.Website,
            createMerchantRequest.OaUrl
        );

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<Merchant>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<Merchant>> GetMerchantByUserId(
        [AsParameters] MerchantServices service
    )
    {
        var id = service.IdentityService.GetCurrentUser();

        var query = new GetMerchantByUserIdQuery(id);

        var result = await service.Mediator.Send(query);

        return ApiResponseSuccess<Merchant>.BuildSuccess(result);

    }

    public static async Task<ApiResponseSuccess<Merchant>> GetMerchantById(
        Guid id,
        [AsParameters] MerchantServices service
    )
    {
        var query = new GetMerchantByIdQuery(id);

        var result = await service.Mediator.Send(query);

        return ApiResponseSuccess<Merchant>.BuildSuccess(result);
    }
}

