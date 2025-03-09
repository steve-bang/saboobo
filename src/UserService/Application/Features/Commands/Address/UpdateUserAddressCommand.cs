
namespace SaBooBo.UserService.Application.Features.Commands;

public record UpdateUserAddressCommand(
    Guid Id,
    Guid UserId,
    string ContactName,
    string PhoneNumber,
    string AddressLine1,
    string AddressLine2,
    string City,
    string State,
    string Country,
    bool IsDefault
) : IRequest<Guid>;