
using SaBooBo.MerchantService.Domain.Repositories;

namespace MerchantService.Application.Features.Commands;

public class CreateMerchantCommandHandler(
    IMerchantRepository _merchantRepository
) : IRequestHandler<CreateMerchantCommand, Merchant>
{
    public async Task<Merchant> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
    {
        var merchant = Merchant.Create(
            request.UserId,
            request.Name,
            request.Description,
            request.EmailAddress,
            request.PhoneNumber,
            request.Address,
            request.LogoUrl,
            request.CoverUrl,
            request.Website,
            request.OAUrl
        );

        await _merchantRepository.CreateAsync(merchant);

        await _merchantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return merchant;
    }
}
