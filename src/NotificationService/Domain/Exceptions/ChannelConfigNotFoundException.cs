


using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.NotificationService.Domain.Errors;

namespace SaBooBo.NotificationService.Domain.Exceptions
{

    public class ChannelConfigNotFoundException : BadRequestException
    {
        public ChannelConfigNotFoundException(Guid id) : base(
            ChannelConfigErrors.ChannelConfigNotFound,
            $"ChannelConfig with id {id} not found",
            "The channel config with the specified id was not found, please check the id and try again."
        )
        {
        }

    }
}