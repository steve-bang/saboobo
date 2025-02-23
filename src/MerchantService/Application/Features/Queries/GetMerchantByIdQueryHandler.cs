

namespace MerchantService.Application.Features.Queries;

public class GetMerchantByIdQueryHandler(
    IMerchantRepository merchantRepository
) : IRequestHandler<GetMerchantByIdQuery, Merchant>
{

    public async Task<Merchant> Handle(GetMerchantByIdQuery request, CancellationToken cancellationToken)
    {
        var merchant = await merchantRepository.GetByIdAsync(request.Id);

        if (merchant is null) throw new MerchantNotFoundException(request.Id);

        return merchant;
    }
}