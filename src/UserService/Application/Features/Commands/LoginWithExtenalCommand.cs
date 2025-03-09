
using SaBooBo.UserService.Domain.AggregatesModel;

namespace SaBooBo.UserService.Application.Features.Commands;

public record LoginWithExtenalCommand(
    Guid MerchantId,
    ExternalProviderAccount Provider,
    string Metadata
) : IRequest<LoginUserResponse>;