
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Application.Features.Queries;

public record GetCategoryByIdQueryHandler(
    ICategoryRepository CategoryRepository
) : IRequestHandler<GetCategoryByIdQuery, Category>
{
    public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await CategoryRepository.GetByIdAsync(request.Id);
    }
}