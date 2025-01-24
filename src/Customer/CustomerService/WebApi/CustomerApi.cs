
using Microsoft.AspNetCore.Mvc;
using SaBooBo.CustomerService.Application.Features.Commands;
using SaBooBo.CustomerService.Application.Features.Queries;
using SaBooBo.Domain.Shared.ApiResponse;

namespace SaBooBo.CustomerService.WebApi;

public class CustomerServices(
    IMediator mediator,
    ILogger<CustomerServices> logger
)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<CustomerServices> Logger { get; } = logger;
}


public static class CustomerApi
{
    public static RouteGroupBuilder MapCustomerApi(this IEndpointRouteBuilder builder)
    {
        var apiCustomer = builder.MapGroup("api/v1/customers");

        // Create a new customer
        // POST api/v1/customers
        apiCustomer.MapPost("", CreateCustomer);

        apiCustomer.MapGet("by-phone/{phoneNumber}", GetCustomerByPhoneNumber);

        apiCustomer.MapPut("{id}", UpdateById);

        return apiCustomer;
    }

    public static async Task<IResult> CreateCustomer(
        [FromBody] CreateCustomerCommand createCustomerCommand,
        [AsParameters] CustomerServices service
    )
    {
        Guid result = await service.Mediator.Send(createCustomerCommand);

        return Results.Created("api/v1/customers", ApiResponseSuccess<Guid>.BuildCreated(result));
    }

    public static async Task<ApiResponseSuccess<Customer>> GetCustomerByPhoneNumber(
        string phoneNumber,
        [AsParameters] CustomerServices service
    )
    {
        var query = new GetCustomerByPhoneNumberQuery(phoneNumber);

        var result = await service.Mediator.Send(query);

        return ApiResponseSuccess<Customer>.BuildSuccess(result);
    }

    public static async Task<ApiResponseSuccess<Customer>> UpdateById(
        Guid id,
        [FromBody] CustomerUpdateRequest payloadRequest,
        [AsParameters] CustomerServices service
    )
    {
        var updateCustomerCommand = new UpdateCustomerCommand(
            id,
            payloadRequest.Name,
            payloadRequest.PhoneNumber,
            payloadRequest.EmailAddress,
            payloadRequest.AvatarUrl,
            payloadRequest.DateOfBirth,
            payloadRequest.Gender
        );

        var result = await service.Mediator.Send(updateCustomerCommand);

        return ApiResponseSuccess<Customer>.BuildSuccess(result);
    }
}

public record CustomerUpdateRequest(
    string Name,
    string PhoneNumber,
    string? EmailAddress,
    string? AvatarUrl,
    DateOnly? DateOfBirth,
    Gender? Gender
);