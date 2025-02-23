
using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.MerchantService.Domain.Errors;

namespace SaBooBo.MerchantService.Domain.Exceptions
{
    public class MerchantProviderAlreadyConfigureException : BadRequestException
    {
        public MerchantProviderAlreadyConfigureException(Guid id) : base(
            MerchantProviderSettingErrors.AlreadyConfigure,
            $"Merchant Provider with merchant id {id} already configured.",
            "The merchant provider with the specified id was already configured. Please check the id and try again."
        )
        {
        }

    }
}