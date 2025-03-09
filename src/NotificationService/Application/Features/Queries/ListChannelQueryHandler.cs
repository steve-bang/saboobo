
using MediatR;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Domain.Repositories;

namespace SaBooBo.NotificationService.Application.Features.Queries
{
    public record ListChannelQueryHandler(
        IChannelRepository ChannelRepository
    ) : IRequestHandler<ListChannelQuery, List<Channel>>
    {
        public async Task<List<Channel>> Handle(ListChannelQuery request, CancellationToken cancellationToken)
        {
            return await ChannelRepository.GetAllAsync(cancellationToken);
        }
    }
}