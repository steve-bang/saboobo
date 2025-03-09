
using MediatR;
using SaBooBo.NotificationService.Domain.AggregatesModel;

namespace SaBooBo.NotificationService.Application.Features.Queries
{
    public record ListChannelQuery() : IRequest<List<Channel>>;
}