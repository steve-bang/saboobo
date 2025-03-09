
using MediatR;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Domain.Exceptions;
using SaBooBo.NotificationService.Domain.Repositories;

namespace SaBooBo.NotificationService.Application.Features.Queries;

public class GetChanneConfigByMerchantQueryHandler : IRequestHandler<GetChanneConfigByMerchantQuery, ChannelConfig>
{
    private readonly IChannelConfigRepository _channelConfigRepository;

    public GetChanneConfigByMerchantQueryHandler(IChannelConfigRepository channelConfigRepository)
    {
        _channelConfigRepository = channelConfigRepository;
    }

    public async Task<ChannelConfig> Handle(GetChanneConfigByMerchantQuery request, CancellationToken cancellationToken)
    {
        var chanelConfig = await _channelConfigRepository.GetByMerchantIdAndChannelIdAsync(request.MerchantId, request.ChannelId, cancellationToken);

        if (chanelConfig == null)
        {
            throw new ChannelConfigNotFoundException(request.MerchantId);
        }

        return chanelConfig;
    }
}