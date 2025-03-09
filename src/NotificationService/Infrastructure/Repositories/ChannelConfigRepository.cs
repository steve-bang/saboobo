
using Microsoft.EntityFrameworkCore;
using SaBooBo.Domain.Shared;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Domain.Repositories;

namespace SaBooBo.NotificationService.Infrastructure.Repositories;

public class ChannelConfigRepository(
    NotificationAppContext _context
) : IChannelConfigRepository
{
    public IUnitOfWork UnitOfWork => _context;

    public async Task CreateAsync(ChannelConfig channelConfig, CancellationToken cancellationToken = default)
    {
        await _context.ChannelConfigs.AddAsync(channelConfig, cancellationToken);
    }

    public async Task<ChannelConfig?> GetByIdAsync(Guid channelConfigId, CancellationToken cancellationToken = default)
    {
        return await _context.ChannelConfigs.FindAsync(new object[] { channelConfigId }, cancellationToken);
    }

    public ChannelConfig Update(ChannelConfig channel, CancellationToken cancellationToken = default)
    {
        var result = _context.ChannelConfigs.Update(channel);

        return result.Entity;
    }

    public async Task<List<ChannelConfig>> GetChannelsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ChannelConfigs.ToListAsync(cancellationToken);
    }

    public bool Delete(ChannelConfig channelConfig)
    {
        _context.ChannelConfigs.Remove(channelConfig);
        return true;
    }

    public Task<ChannelConfig?> GetByMerchantIdAndChannelIdAsync(Guid merchantId, Guid channelId, CancellationToken cancellationToken = default)
    {
        return _context.ChannelConfigs.FirstOrDefaultAsync(x => x.MerchantId == merchantId && x.ChannelId == channelId, cancellationToken);
    }
}