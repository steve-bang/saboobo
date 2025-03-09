
using MediatR;
using SaBooBo.NotificationService.Domain.AggregatesModel;

namespace SaBooBo.NotificationService.Application.Features.Commands
{
    public record CreateChannelCommand(Channel Channel) : IRequest<Channel>;
}