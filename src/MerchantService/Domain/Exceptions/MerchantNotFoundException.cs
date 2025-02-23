
using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.MerchantService.Domain.Errors;

namespace SaBooBo.MerchantService.Domain.Exceptions
{
    public class MerchantNotFoundException : NotFoundException
    {
        public MerchantNotFoundException(Guid id) : base(
            MerchantErrors.NotFound,
            $"Merchant with Id: {id} not found.",
            "The merchant with the specified Id was not found. Please check the Id and try again."
        )
        {
        }

        public MerchantNotFoundException(string code) : base(
            MerchantErrors.NotFound,
            $"Merchant with code: {code} not found.",
            "The merchant with the specified code was not found. Please check the code and try again."
        )
        {
        }
    }
}