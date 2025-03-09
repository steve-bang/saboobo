
using SaBooBo.Domain.Shared;
using SaBooBo.NotificationService.Domain.AggregatesModel;

namespace SaBooBo.NotificationService.Domain.Repositories;

public interface IChannelRepository : IRepository
{
    Task CreateAsync(Channel channel, CancellationToken cancellationToken = default);

    Task<Channel?> GetChannelAsync(Guid channelId, CancellationToken cancellationToken = default);

    Task<Channel?> GetChannelByNameAsync(string channelName, CancellationToken cancellationToken = default);

    Task<Channel?> GetChannelByTypeAsync(ChannelType channelType, CancellationToken cancellationToken = default);

    Channel Update(Channel channel, CancellationToken cancellationToken = default);

    Task<List<Channel>> GetAllAsync(CancellationToken cancellationToken = default);
}