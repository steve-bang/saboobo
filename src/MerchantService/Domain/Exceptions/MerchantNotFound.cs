
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.MerchantService.Domain.Exceptions
{
    public class MerchantNotFoundException : NotFoundException
    {
        public MerchantNotFoundException(Guid id) : base(
            "Merchant_Not_Found",
            $"Merchant with Id: {id} not found.",
            "The merchant with the specified Id was not found. Please check the Id and try again."
        )
        {
        }
    }
}