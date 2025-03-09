
using MediatR;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Models.DTO;

namespace SaBooBo.NotificationService.Application.Features.Commands
{
    public record UpdateChannelConfigCommand(Guid Id, ChannelConfigCommandRequestDto ChannelConfig) : IRequest<ChannelConfig>;
}

