
namespace SaBooBo.Domain.Shared;

public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }
}