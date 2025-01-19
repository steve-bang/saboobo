

namespace SaBooBo.Product.Application.Features.Commands;

public record DeleteCategoryByIdCommandHandler(
    ICategoryRepository CategoryRepository
) : IRequestHandler<DeleteCategoryByIdCommand, bool>
{
    public async Task<bool> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await CategoryRepository.DeleteByIdAsync(request.Id);

        if(result) await CategoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return result;
    }
}