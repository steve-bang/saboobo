
using Microsoft.AspNetCore.Mvc;
using SaBooBo.Domain.Shared.ApiResponse;
using SaBooBo.UserService.Application.Features.Commands;
using SaBooBo.UserService.Application.Features.Queries;
using SaBooBo.UserService.Application.Models;
using SaBooBo.UserService.Domain.AggregatesModel;

namespace SaBooBo.UserService.Apis;

public static class UserApi
{
    public static RouteGroupBuilder MapUserApi(this IEndpointRouteBuilder builder)
    {
        var apiUser = builder.MapGroup("api/v1/users");

        // GET /api/v1/users/{id}
        // GET /api/v1/users/me
        // Get user by id or get current user
        apiUser.MapGet("{id}", GetById);

        // GET /api/v1/users/check-phone-number
        // Check if phone number is already registered
        apiUser.MapPost("check-phone-number", CheckPhoneNumber);

        // POST /api/v1/users/{userId}/addresses
        // Create new user address
        apiUser.MapPost("{userId}/addresses", CreateUserAddress);

        // GET /api/v1/users/{userId}/addresses
        // List user addresses
        apiUser.MapGet("{userId}/addresses", ListUserAddresses);

        //// DELETE /api/v1/users/{userId}/addresses/{addressId}
        //// Delete user address by id
        apiUser.MapDelete("{userId}/addresses/{addressId}", DeleteUserAddresses);

        return apiUser;
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

    public static async Task<IResult> CreateUserAddress(
        Guid userId,
        [FromBody] UserAddressRequest payloadRequest,
        [AsParameters] UserServices service
    )
    {
        var command = new CreateUserAddressCommand(
            userId,
            payloadRequest.ContactName,
            payloadRequest.PhoneNumber,
            payloadRequest.AddressLine1,
            payloadRequest.AddressLine2,
            payloadRequest.City,
            payloadRequest.State,
            payloadRequest.Country,
            payloadRequest.IsDefault
        );

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<Guid>.BuildCreated(String.Empty, result);
    }

    public static async Task<ApiResponseSuccess<List<UserAddress>>> ListUserAddresses(
        Guid userId,
        [AsParameters] UserServices service
    )
    {
        var command = new ListUserAddressByUserIdQuery(
            userId
        );

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<List<UserAddress>>.BuildSuccess(result);
    }

    public static async Task<IResult> DeleteUserAddresses(
        Guid userId,
        Guid addressId,
        [AsParameters] UserServices service
    )
    {
        var command = new DeleteUserAddressCommand(
            userId,
            addressId
        );

        await service.Mediator.Send(command);

        return ApiResponseSuccess<bool>.BuildNoContent();
    }

}


public record UserAddressRequest(
    string ContactName,
    string PhoneNumber,
    string AddressLine1,
    string AddressLine2,
    string City,
    string State,
    string Country,
    bool IsDefault
);