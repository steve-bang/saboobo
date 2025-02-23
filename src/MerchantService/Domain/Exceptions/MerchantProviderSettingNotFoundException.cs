
using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.MerchantService.Domain.Errors;

namespace SaBooBo.MerchantService.Domain.Exceptions
{
    public class MerchantProviderSettingNotFoundException : NotFoundException
    {
        public MerchantProviderSettingNotFoundException(Guid id) : base(
            MerchantProviderSettingErrors.NotFound,
            $"Merchant Provider Setting with Id: {id} not found.",
            "The provider setting with the specified Id was not found. Please check the Id and try again."
        )
        {
        }

    }
}