
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Application.Features.Commands;

public class CreateCategoryCommandHandler(
    ICategoryRepository CategoryRepository
) : IRequestHandler<CreateCategoryCommand, Guid>
{
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(
            merchantId: request.MerchantId,
            code: request.Code,
            name: request.Name,
            description: request.Description,
            iconUrl: request.IconUrl
        );

        await CategoryRepository.CreateAsync(category);

        await CategoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return category.Id;
        
    }
}