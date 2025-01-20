
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Application.Features.Commands;

public class UpdateCategoryCommandHandler(
    ICategoryRepository CategoryRepository
) : IRequestHandler<UpdateCategoryCommand, Category>
{
    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await CategoryRepository.GetByIdAsync(request.Id);

        if (category is null) 
            throw new NotFoundException(
                ErrorCodes.CategoryNotFound,
                "The category was not found.",
                "The category was not found in the system, please check your id request."
            );

        category.Update(
            code: request.Code,
            name: request.Name,
            description: request.Description,
            iconUrl: request.IconUrl
        );

        CategoryRepository.Update(category);

        await CategoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return category;
    }
}