
namespace SaBooBo.UserService.Application.Features.Commands;

public record CheckPhoneNumberUserQuery(Guid? MerchantId, string PhoneNumber) : IRequest<bool>;