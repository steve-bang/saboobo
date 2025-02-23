
namespace SaBooBo.CartService.Domain.AggregatesModel;

public enum CartStatus : byte
{
    New = 0,
    Pending = 1,
    Completed = 2,
    Cancelled = 3
}