

namespace SaBooBo.Product.Application.Features.Commands;

public class DeleteProductCommandHandler(
    IProductRepository ProductRepository
) : IRequestHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await ProductRepository.GetByIdAsync(request.Id);

        if (product == null) return false;

        // Deletes the product
        var result = ProductRepository.Delete(product);

        // Checks if the product was deleted
        if (result)
            product.Delete();


        // Saves the changes
        await ProductRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return result;
    }
}