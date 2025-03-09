
using MediatR;
using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Domain.Exceptions;
using SaBooBo.NotificationService.Domain.Repositories;

namespace SaBooBo.NotificationService.Application.Features.Commands
{
    public class UpdateChannelConfigCommandHandler(
        IChannelConfigRepository _channelConfigRepository
    ) : IRequestHandler<UpdateChannelConfigCommand, ChannelConfig>
    {
        public async Task<ChannelConfig> Handle(UpdateChannelConfigCommand request, CancellationToken cancellationToken)
        {
            var channelConfig = await _channelConfigRepository.GetByIdAsync(request.Id, cancellationToken);

            if (channelConfig == null)
            {
                throw new ChannelConfigNotFoundException(request.Id);
            }

            // Update the channel config
            channelConfig.Update(
                request.ChannelConfig.Name,
                request.ChannelConfig.Description,
                request.ChannelConfig.Metadata
            );

            _channelConfigRepository.Update(channelConfig);

            await _channelConfigRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return channelConfig;
        }
    }
}