
namespace SaBooBo.CustomerService.Application.Features.Commands;

public record CreateCustomerCommand(
    Guid MerchantId,
    string Name,
    string PhoneNumber,
    string? EmailAddress,
    string? AvatarUrl,
    DateOnly? DateOfBirth,
    Gender Gender
) : IRequest<Guid>;