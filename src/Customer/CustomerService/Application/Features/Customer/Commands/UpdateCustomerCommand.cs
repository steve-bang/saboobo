
namespace SaBooBo.CustomerService.Application.Features.Commands;

public record UpdateCustomerCommand(
    Guid Id,
    string Name,
    string PhoneNumber,
    string? EmailAddress,
    string? AvatarUrl,
    DateOnly? DateOfBirth,
    Gender? Gender
) : IRequest<Customer>;