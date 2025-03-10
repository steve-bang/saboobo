
using MediatR;
using SaBooBo.Domain.Shared.Utils;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Domain.Repositories;
using SaBooBo.OrderService.Clients;

namespace SaBooBo.NotificationService.Application.Features.Commands;

public class OrderCompletedCommandHandler(
    IZaloClient _zaloClient,
    IChannelRepository _channelRepository,
    IChannelConfigRepository _channelConfigRepository
) : IRequestHandler<OrderCompletedCommand, bool>
{
    public async Task<bool> Handle(OrderCompletedCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var channel = await _channelRepository.GetChannelByTypeAsync(ChannelType.Zalo);

            if (channel == null)
            {
                LoggingUtil.WriteLog($"Channel Zalo not found with type {ChannelType.Zalo}");
                return false;
            }

            // Get zalo channel config
            var zaloChannelConfig = await _channelConfigRepository.GetByMerchantIdAndChannelIdAsync(request.Order.MerchantId, channel.Id);
            if (zaloChannelConfig == null)
            {
                LoggingUtil.WriteLog($"Zalo channel config not found for merchant {request.Order.MerchantId}");
                return false;
            }

            if (!string.IsNullOrEmpty(request.Order.ZaloOrderId))
            {
                LoggingUtil.WriteLog($"Start update order status COD for order {request.Order.Id} with zalo order id {request.Order.ZaloOrderId}");

                await _zaloClient.UpdateOrderStatusCODAsync(
                    miniAppId: zaloChannelConfig.Metadata[ZaloKeysConfig.MiniAppId],
                    privateKey: zaloChannelConfig.Metadata[ZaloKeysConfig.PrivateKey],
                    orderId: request.Order.ZaloOrderId,
                    resultCode: ResultCodes.Success
                );
            }

            return true;
        }
        catch (Exception ex)
        {
            LoggingUtil.WriteLog($"Error when send message to zalo for order {request.Order.Id}: {ex.Message}");
            LoggingUtil.WriteLog(ex);
            return false;
        }

    }
}