
using Microsoft.EntityFrameworkCore;
using SaBooBo.Domain.Shared;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Domain.Repositories;

namespace SaBooBo.NotificationService.Infrastructure.Repositories;

public class ChannelRepository(
    NotificationAppContext _context
) : IChannelRepository
{
    public IUnitOfWork UnitOfWork => _context;


    public async Task CreateAsync(Channel channel, CancellationToken cancellationToken = default)
    {
        await _context.Channels.AddAsync(channel, cancellationToken);
    }

    public async Task<Channel?> GetChannelAsync(Guid channelId, CancellationToken cancellationToken = default)
    {
        return await _context.Channels.FindAsync(new object[] { channelId }, cancellationToken);
    }

    public async Task<Channel?> GetChannelByTypeAsync(ChannelType channelType, CancellationToken cancellationToken = default)
    {
        return await _context.Channels.FirstOrDefaultAsync(x => x.ChannelType == channelType, cancellationToken)!;
    }

    public Channel Update(Channel channel, CancellationToken cancellationToken = default)
    {
        var result = _context.Channels.Update(channel);

        return result.Entity;
    }

    public async Task<List<Channel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Channels.ToListAsync(cancellationToken);
    }

    public Task<Channel?> GetChannelByNameAsync(string channelName, CancellationToken cancellationToken = default)
    {
        return _context.Channels.FirstOrDefaultAsync(x => x.Name == channelName, cancellationToken);
    }
}