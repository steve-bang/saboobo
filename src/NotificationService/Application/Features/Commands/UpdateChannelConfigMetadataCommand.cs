
using MediatR;
using SaBooBo.NotificationService.Domain.AggregatesModel;

namespace SaBooBo.NotificationService.Application.Features.Commands
{
    public record UpdateChannelConfigMetadataCommand(Guid MerchantId, ChannelType ChannelType, IDictionary<string, string> Metadata) : IRequest<bool>;
}

