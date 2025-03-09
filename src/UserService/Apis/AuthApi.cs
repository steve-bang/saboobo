
using Microsoft.AspNetCore.Mvc;
using SaBooBo.Domain.Shared.ApiResponse;
using SaBooBo.UserService.Application.Features.Commands;
using SaBooBo.UserService.Domain.AggregatesModel;

namespace SaBooBo.UserService.Apis;

public static class AuthApi
{
    public static RouteGroupBuilder MapAuthApi(this IEndpointRouteBuilder builder)
    {
        var apiAuth = builder.MapGroup("api/v1/auth");

        // POST api/v1/auth/register
        // Register new user with phone number and password
        apiAuth.MapPost("register", Register);

        // POST api/v1/auth/login
        // Login with phone number and password
        apiAuth.MapPost("login", Login);

        // POST api/v1/auth/login/external-provider
        // Login with external provider (Zalo, Facebook, Google, ...)
        apiAuth.MapPost("login/external-provider", LoginWithExternal);

        return apiAuth;
    }

    public static async Task<ApiResponseSuccess<Guid>> Register(
        [FromBody] RegisterRequest registerRequest,
        [AsParameters] UserServices service
    )
    {
        var command = new RegisterUserCommand(
            registerRequest.MerchantId,
            registerRequest.PhoneNumber,
            registerRequest.Password,
            registerRequest.ConfirmPassword,
            registerRequest.Name
        );

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<Guid>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<LoginUserResponse>> Login(
        [FromBody] LoginRequest loginRequest,
        [AsParameters] UserServices service
    )
    {
        var command = new LoginUserCommand(
            loginRequest.PhoneNumber,
            loginRequest.Password
        );

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<LoginUserResponse>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<LoginUserResponse>> LoginWithExternal(
        [FromBody] LoginWithExternalRequest loginWithExternalRequest,
        [AsParameters] UserServices service
    )
    {
        var command = new LoginWithExtenalCommand(
            loginWithExternalRequest.MerchantId,
            loginWithExternalRequest.Provider,
            loginWithExternalRequest.Metadata
        );

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<LoginUserResponse>.BuildSuccess(result);
    }
}

public record LoginRequest(string PhoneNumber, string Password);

public record RegisterRequest(
    Guid? MerchantId,
    string PhoneNumber,
    string Password,
    string ConfirmPassword,
    string Name
);

public record LoginWithExternalRequest(
    Guid MerchantId,
    ExternalProviderAccount Provider, 
    string Metadata
);