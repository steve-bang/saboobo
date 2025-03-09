
using MediatR;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Domain.Repositories;

namespace SaBooBo.NotificationService.Application.Features.Commands
{
    public class CreateChannelConfigCommandHandler(
        IChannelConfigRepository _channelConfigRepository
    ) : IRequestHandler<CreateChannelConfigCommand, ChannelConfig>
    {
        public async Task<ChannelConfig> Handle(CreateChannelConfigCommand request, CancellationToken cancellationToken)
        {
            var channelConfig = await _channelConfigRepository.GetByMerchantIdAndChannelIdAsync(request.MerchantId, request.ChannelId, cancellationToken);
            if (channelConfig != null)
            {
                channelConfig.Update(
                    name: request.ChannelConfig.Name,
                    description: request.ChannelConfig.Description,
                    metadata: request.ChannelConfig.Metadata
                );

                _channelConfigRepository.Update(channelConfig);

            }
            else
            {
                channelConfig = ChannelConfig.Create(
                    merchantId: request.MerchantId,
                    channelId: request.ChannelId,
                    name: request.ChannelConfig.Name,
                    description: request.ChannelConfig.Description,
                    metadata: request.ChannelConfig.Metadata
                );

                await _channelConfigRepository.CreateAsync(channelConfig, cancellationToken);
            }


            await _channelConfigRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return channelConfig;
        }
    }
}