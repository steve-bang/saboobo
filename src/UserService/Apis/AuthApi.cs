
using Microsoft.AspNetCore.Mvc;
using SaBooBo.Domain.Shared.ApiResponse;
using SaBooBo.UserService.Application.Features.Commands;

namespace SaBooBo.UserService.Apis;

public static class AuthApi
{
    public static RouteGroupBuilder MapAuthApi(this IEndpointRouteBuilder builder)
    {
        var apiAuth = builder.MapGroup("api/v1/auth");

        apiAuth.MapPost("register", Register);

        apiAuth.MapPost("login", Login);

        return apiAuth;
    }

    public static async Task<ApiResponseSuccess<Guid>> Register(
        [FromBody] RegisterRequest registerRequest,
        [AsParameters] UserServices service
    )
    {
        var command = new RegisterUserCommand(
            registerRequest.PhoneNumber,
            registerRequest.Password,
            registerRequest.ConfirmPassword
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
}

public record LoginRequest(string PhoneNumber, string Password);

public record RegisterRequest(
    string PhoneNumber,
    string Password,
    string ConfirmPassword
);