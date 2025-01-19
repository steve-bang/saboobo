
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Application.Features.Queries;

public record ListCategoryQueryHandler(
    ICategoryRepository CategoryRepository
) : IRequestHandler<ListCategoryQuery, List<Category>>
{
    public async Task<List<Category>> Handle(ListCategoryQuery request, CancellationToken cancellationToken)
    {
        return await CategoryRepository.ListAllAsync(request.MerchantId);
    }
}