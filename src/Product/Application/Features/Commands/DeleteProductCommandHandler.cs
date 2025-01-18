

namespace SaBooBo.Product.Application.Features.Commands;

public class DeleteProductCommandHandler(
    IProductRepository ProductRepository
) : IRequestHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = await ProductRepository.DeleteByIdAsync(request.Id);

        await ProductRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return result;
    }
}