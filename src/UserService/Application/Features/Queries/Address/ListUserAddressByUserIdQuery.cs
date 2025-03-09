
using SaBooBo.UserService.Domain.AggregatesModel;

namespace SaBooBo.UserService.Application.Features.Queries;

public record ListUserAddressByUserIdQuery(Guid UserId) : IRequest<List<UserAddress>>;