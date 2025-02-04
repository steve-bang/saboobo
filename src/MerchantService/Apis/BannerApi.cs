
using MerchantService.Application.Features.Commands;
using MerchantService.Application.Features.Queries;
using Microsoft.AspNetCore.Mvc;
using SaBooBo.Domain.Shared.ApiResponse;

namespace SaBooBo.MerchantService.Apis;

public static class BannerApi
{
    public static RouteGroupBuilder MapBannerApi(this IEndpointRouteBuilder builder)
    {
        var apiBanner = builder.MapGroup("api/v1/merchants/{merchantId}/banners");

        // POST /api/v1/merchants/{merchantId}/banners
        apiBanner.MapPost("", CreateBanner);

        // GET /api/v1/merchants/{merchantId}/banners
        apiBanner.MapGet("", ListBannersByMerchantId);

        // PUT /api/v1/merchants/{merchantId}/banners
        apiBanner.MapPut("", UpdateBanners);

        // DELETE /api/v1/merchants/{merchantId}/banners/{id}
        apiBanner.MapDelete("{id}", DeleteBanner);

        return apiBanner;
    }

    public static async Task<ApiResponseSuccess<List<BannerResponse>>> ListBannersByMerchantId(
        Guid merchantId,
        [AsParameters] MerchantServices service
    )
    {
        var query = new ListBannersByMerchantIdQuery(merchantId);

        var result = await service.Mediator.Send(query);

        return ApiResponseSuccess<List<BannerResponse>>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<Guid>> CreateBanner(
        Guid merchantId,
        BannerCommandRequest request,
        [AsParameters] MerchantServices service
    )
    {
        var command = new CreateBannerCommand(merchantId, request.Name, request.ImageUrl, request.Link);

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<Guid>.BuildCreated(result);
    }

    public static async Task<ApiResponseSuccess<bool>> DeleteBanner(
        Guid merchantId,
        Guid id,
        [AsParameters] MerchantServices service
    )
    {
        var command = new DeleteBannerCommand(merchantId, id);

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<bool>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<bool>> UpdateBanners(
        Guid merchantId,
        List<BannersUpdateRequest> request,
        [AsParameters] MerchantServices service
    )
    {
        var command = new UpdateBannersCommand(merchantId, request);

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<bool>.BuildSuccess(result);
    }
}

public record BannerCommandRequest(
    string Name,
    string ImageUrl,
    string Link
);

public record BannersUpdateRequest(
    Guid Id,
    string Name,
    string ImageUrl,
    string Link
);