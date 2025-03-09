
using System.Text.Json;
using MediatR;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.NotificationService.Domain.Exceptions;
using SaBooBo.NotificationService.Domain.Repositories;
using SaBooBo.NotificationService.Models;
using SaBooBo.OrderService.Clients;

namespace SaBooBo.NotificationService.Application.Features.Commands
{
    public class UpdateChannelConfigMetadataCommandHandler(
        IChannelRepository _channelRepository,
        IChannelConfigRepository _channelConfigRepository,
        IZaloClient _zaloClient
    ) : IRequestHandler<UpdateChannelConfigMetadataCommand, bool>
    {
        public async Task<bool> Handle(UpdateChannelConfigMetadataCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Updating channel config metadata for merchant {request.MerchantId} and channel {request.ChannelType}");
            Console.WriteLine($"Metadata: {JsonSerializer.Serialize(request.Metadata)}");

            var channel = await _channelRepository.GetChannelByTypeAsync(request.ChannelType, cancellationToken);

            if (channel == null)
            {
                return false;
            }

            ChannelConfig? channelConfig = await _channelConfigRepository.GetByMerchantIdAndChannelIdAsync(request.MerchantId, channel.Id);

            if (channelConfig == null)
            {
                throw new ChannelConfigNotFoundException(request.MerchantId);
            }

            channelConfig.UpdateMetadata(request.Metadata);

            // Handle call to Zalo API to get accessToken and refreshToken for Zalo channel
            await UpdateZaloOAuthConfigMetadata(channelConfig);

            _channelConfigRepository.Update(channelConfig);

            await _channelConfigRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }

        private async Task UpdateZaloOAuthConfigMetadata(ChannelConfig channelConfig)
        {
            Console.WriteLine("Updating Zalo OAuth config metadata.");

            if (channelConfig.IsAccessTokenAvailable())
            {
                Console.WriteLine("Access token is available. No need to update Zalo OAuth config metadata.");
                return;
            }

            // Update metadata config
            ZaloAuthResponseModel? zaloOAuthResult = await _zaloClient.GetAccessTokenAsync(
                secretKey: channelConfig.Metadata[ZaloKeysConfig.SecretKey],
                appId: channelConfig.Metadata[ZaloKeysConfig.AppId],
                oauthCode: channelConfig.Metadata[ZaloKeysConfig.OAuthCode]
            );

            // Check if Zalo OAuth result is null or has error
            if (zaloOAuthResult == null || zaloOAuthResult.IsError())
            {
                // Print error message if Zalo OAuth result is not null
                if (zaloOAuthResult != null) Console.WriteLine($"Zalo OAuth result: {JsonSerializer.Serialize(zaloOAuthResult)}");

                Console.WriteLine("Failed to get access token from Zalo.");
            }
            else
            {
                channelConfig.UpdateZaloOAuthConfig(
                    zaloOAuthResult.AccessToken,
                    zaloOAuthResult.RefreshToken,
                    zaloOAuthResult.ExpiresIn,
                    zaloOAuthResult.RefreshTokenExpiresIn
                );

                Console.WriteLine("Zalo OAuth config metadata updated.");
            }

        }

    }

}