using MediatR;
using SaBooBo.Clients.Shared.Clients;
using SaBooBo.Domain.Common;
using SaBooBo.Domain.Shared.Utils;
using SaBooBo.NotificationService.Domain.Repositories;
using SaBooBo.NotificationService.Domain.Services;

namespace SaBooBo.NotificationService.Application.Features.Commands;

public class SendMessageOrderCommandHandler(
    ISmsService _smsService,
    IChannelConfigRepository _channelConfigRepository,
    IMerchantClient _merchantClient
) : IRequestHandler<SendMessageOrderCommand, bool>
{
    public async Task<bool> Handle(SendMessageOrderCommand request, CancellationToken cancellationToken)
    {

        // Hard code for now.
        // All variables below are hard code for now. In the future, we will get them from the database.
        string userIdZalo = "7279905758396907775";
        string miniZaloAppLink = "https://mini.zalo.me/apps/3147492405195201163/";
        string phoneNumberOwner = "84398295863";

        // Get merchant info
        try
        {
            var merchant = await _merchantClient.GetMerchantAsync(request.Order.MerchantId.ToString());

            LoggingUtil.WriteLog($"Merchant response: {merchant}");

            if (merchant != null)
            {
                phoneNumberOwner = merchant.PhoneNumber;
            }
            else
            {
                LoggingUtil.WriteLog($"Merchant {request.Order.MerchantId} not found. Use default phone number {phoneNumberOwner}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtil.WriteLog($"Error getting merchant {request.Order.MerchantId}: {ex.Message}");
        }


        // Create zalo message template
        ZaloMessageTemplate zaloMessageCompleteTemplate = ZaloMessageTemplate.Create(
            userId: userIdZalo,
            orderId: request.Order.Id,
            status: request.Order.Status,
            shippingTotal: (double?)request.Order.ShippingTotal,
            totalPrice: (double)request.Order.CostTotal,
            miniZaloAppLink: miniZaloAppLink,
            phoneNumberOwner: phoneNumberOwner
        );

        // Send message to zalo
        await _smsService.SendZaloTransactionNotificationAsync(request.Order.MerchantId, zaloMessageCompleteTemplate);

        LoggingUtil.WriteLog($"Send message to zalo for order {request.Order.Id} success");

        await _channelConfigRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}