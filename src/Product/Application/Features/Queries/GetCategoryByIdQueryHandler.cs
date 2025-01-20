
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Application.Features.Queries;

public record GetCategoryByIdQueryHandler(
    ICategoryRepository CategoryRepository
) : IRequestHandler<GetCategoryByIdQuery, Category>
{
    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await CategoryRepository.GetByIdAsync(request.Id);

        if(category is null) throw new NotFoundException(
            ErrorCodes.CategoryNotFound,
            "The category was not found.",
            "The category was not found in the system, please check your id request."
        );

        return category;
    }
}