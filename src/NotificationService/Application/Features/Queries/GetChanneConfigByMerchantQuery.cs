
using MediatR;
using SaBooBo.NotificationService.Domain.AggregatesModel;

namespace SaBooBo.NotificationService.Application.Features.Queries;

public record GetChanneConfigByMerchantQuery(Guid ChannelId, Guid MerchantId) : IRequest<ChannelConfig>;