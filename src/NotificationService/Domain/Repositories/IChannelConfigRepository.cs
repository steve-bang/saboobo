
using SaBooBo.Domain.Shared;
using SaBooBo.NotificationService.Domain.AggregatesModel;

namespace SaBooBo.NotificationService.Domain.Repositories;

public interface IChannelConfigRepository : IRepository
{
    Task CreateAsync(ChannelConfig channelConfig, CancellationToken cancellationToken = default);

    Task<ChannelConfig?> GetByIdAsync(Guid channelConfigId, CancellationToken cancellationToken = default);

    Task<ChannelConfig?> GetByMerchantIdAndChannelIdAsync(Guid merchantId, Guid channelId, CancellationToken cancellationToken = default);

    ChannelConfig Update(ChannelConfig channel, CancellationToken cancellationToken = default);

    Task<List<ChannelConfig>> GetChannelsAsync(CancellationToken cancellationToken = default);

    bool Delete(ChannelConfig channelConfig);
}