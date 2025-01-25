
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

        return apiAuth;
    }

    public static async Task<ApiResponseSuccess<Guid>> Register(
        [FromBody] RegisterRequest loginRequest,
        [AsParameters] UserServices service
    )
    {
        var command = new RegisterUserCommand(
            loginRequest.PhoneNumber,
            loginRequest.Password,
            loginRequest.ConfirmPassword
        );

        var result = await service.Mediator.Send(command);

        return ApiResponseSuccess<Guid>.BuildSuccess(result);
    }
}

public record LoginRequest(string Email, string Password);

public record RegisterRequest(
    string PhoneNumber,
    string Password,
    string ConfirmPassword
);