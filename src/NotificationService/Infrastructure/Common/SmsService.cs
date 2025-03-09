
using SaBooBo.Domain.Common;
using SaBooBo.Domain.Shared.Utils;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Domain.Repositories;
using SaBooBo.NotificationService.Domain.Services;
using SaBooBo.OrderService.Clients;


namespace SaBooBo.NotificationService.Infrastructure.Services;

public class SmsService(
    IChannelConfigRepository _channelConfigRepository,
    IChannelRepository _channelRepository,
    IZaloClient _zaloClient
) : ISmsService
{

    public async Task SendZaloTransactionNotificationAsync(Guid merchantId, ZaloMessageTemplate request)
    {
        var channel = await _channelRepository.GetChannelByTypeAsync(ChannelType.Zalo);

        if (channel == null)
        {
            LoggingUtil.WriteLog("Channel not found when sending Zalo transaction notification", nameof(SmsService));
            return;
        }

        ChannelConfig? channelConfig = await _channelConfigRepository.GetByMerchantIdAndChannelIdAsync(merchantId, channel.Id);

        if (channelConfig == null)
        {
            LoggingUtil.WriteLog("Channel Config not found when sending Zalo transaction notification", nameof(SmsService));
            return;
        }

        var template = request.BuildMessage();

        // Update access token metadata config before sending message
        await UpdateAccessTokenMetadataConfig(channelConfig);

        var metadata = channelConfig.Metadata;

        LoggingUtil.WriteLog($"Sending Zalo transaction notification to {merchantId} with template {template}");

        // Send message
        if (metadata.ContainsKey(ZaloKeysConfig.AccessToken))
        {
            var accessToken = metadata[ZaloKeysConfig.AccessToken];
            await _zaloClient.SendMessageAsync(accessToken, template);
        }
        else
        {
            LoggingUtil.WriteLog("Access token not found when sending Zalo transaction notification", nameof(SmsService));
        }
    }

    private async Task UpdateAccessTokenMetadataConfig(ChannelConfig channelConfig)
    {
        if (channelConfig.IsAccessTokenAvailable())
            return;

        var metadata = channelConfig.Metadata;

        string refreshToken = metadata[ZaloKeysConfig.RefreshToken];
        string appId = metadata[ZaloKeysConfig.AppId];
        string secretKey = metadata[ZaloKeysConfig.SecretKey];

        LoggingUtil.WriteLog($"Updating access token metadata config for {channelConfig.Id}");


        var zaloAuthResponse = await _zaloClient.GetAccessTokenFromRefreshTokenAsync(secretKey, refreshToken, appId);

        if (zaloAuthResponse != null)
        {
            channelConfig.UpdateZaloOAuthConfig(
                zaloAuthResponse.AccessToken,
                zaloAuthResponse.RefreshToken,
                zaloAuthResponse.ExpiresIn,
                zaloAuthResponse.RefreshTokenExpiresIn
            );
        }

        _channelConfigRepository.Update(channelConfig);
    }
}