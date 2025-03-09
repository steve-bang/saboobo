

using Newtonsoft.Json;
using SaBooBo.UserService.Domain.AggregatesModel;

namespace SaBooBo.UserService.Application.Features.Commands
{
    public class LoginWithExtenalCommandHandlerValidator : AbstractValidator<LoginWithExtenalCommand>
    {
        public LoginWithExtenalCommandHandlerValidator()
        {

            RuleFor(x => x.Metadata)
                .NotEmpty()
                .WithMessage("The metadata is required")
                .WithErrorCode("Metadata_Requied")
                .Must((x, metadata) => ValidMetadataByType(x.Provider, metadata))
                .WithMessage("The metadata is invalid, the reason may be the provider is invalid or missing field id or phone number.")
                .WithErrorCode("Metadata_Invalid");

            RuleFor(x => x.Provider)
                .IsInEnum()
                .WithMessage("The provider is invalid")
                .WithErrorCode("Provider_Invalid");


            RuleFor(x => x.MerchantId)
                .NotEmpty()
                .WithMessage("The merchant id is required")
                .WithErrorCode("MerchantId_Required");
        }

        private bool ValidMetadataByType(ExternalProviderAccount provider, string metadata)
        {
            switch (provider)
            {
                case ExternalProviderAccount.Zalo:
                    try
                    {
                        var zaloMetadata = JsonConvert.DeserializeObject<UserInfoZaloProvider>(metadata);
                        return zaloMetadata != null && !string.IsNullOrEmpty(zaloMetadata.Id) && !string.IsNullOrEmpty(zaloMetadata.PhoneNumber);
                    }
                    catch
                    {
                        return false;
                    }
                case ExternalProviderAccount.Facebook:
                    return true;
                default:
                    return true;
            }
        }
    }
}