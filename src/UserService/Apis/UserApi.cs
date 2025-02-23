
using Microsoft.AspNetCore.Mvc;
using SaBooBo.Domain.Shared.ApiResponse;
using SaBooBo.UserService.Application.Features.Commands;
using SaBooBo.UserService.Application.Models;
using SaBooBo.UserService.Domain.AggregatesModel;

namespace SaBooBo.UserService.Apis;

public static class UserApi
{
    public static RouteGroupBuilder MapUserApi(this IEndpointRouteBuilder builder)
    {
        var apiAuth = builder.MapGroup("api/v1/users");

        // GET /api/v1/users/{id}
        // GET /api/v1/users/me
        // Get user by id or get current user
        apiAuth.MapGet("{id}", GetById);

        apiAuth.MapPost("check-phone-number", CheckPhoneNumber);

        return apiAuth;
    }

    public static async Task<ApiResponseSuccess<UserResponse>> GetById(
        string id,
        [AsParameters] UserServices service
    )
    {
        // If id is "me", get the current user
        if (id == "me")
        {
            id = service.IdentityService.GetCurrentUser().ToString();
        }

        var query = new GetUserByIdQuery(
            Guid.Parse(id)
        );

        var result = await service.Mediator.Send(query);

        return ApiResponseSuccess<UserResponse>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<bool>> CheckPhoneNumber(
        [FromBody] CheckPhoneNumberUserQuery command,
        [AsParameters] UserServices service
    )
    {
        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<bool>.BuildSuccess(result);
    }

}

