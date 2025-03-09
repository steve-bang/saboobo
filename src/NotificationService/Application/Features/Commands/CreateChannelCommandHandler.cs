
using MediatR;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Domain.Repositories;

namespace SaBooBo.NotificationService.Application.Features.Commands
{
    public record CreateChannelCommandHandler(
        IChannelRepository ChannelRepository
    ) : IRequestHandler<CreateChannelCommand, Channel>
    {
        public async Task<Channel> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = Channel.Create(
                request.Channel.ChannelType,
                request.Channel.Name,
                request.Channel.Description
            );


            await ChannelRepository.CreateAsync(channel, cancellationToken);

            await ChannelRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return channel;
        }
    }
}