
using SaBooBo.UserService.Domain.AggregatesModel;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Application.Features.Queries;

public class ListUserAddressByUserIdQueryHandler(
    IUserAddressRepository _userAddressRepository
) : IRequestHandler<ListUserAddressByUserIdQuery, List<UserAddress>>
{

    public async Task<List<UserAddress>> Handle(ListUserAddressByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _userAddressRepository.GetByUserIdAsync(request.UserId);
    }
}