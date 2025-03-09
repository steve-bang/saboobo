
using MediatR;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Models.DTO;

namespace SaBooBo.NotificationService.Application.Features.Commands
{
    public record CreateChannelConfigCommand(Guid ChannelId, Guid MerchantId, ChannelConfigCommandRequestDto ChannelConfig) : IRequest<ChannelConfig>;
}

