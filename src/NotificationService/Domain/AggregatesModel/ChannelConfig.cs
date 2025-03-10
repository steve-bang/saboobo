
using SaBooBo.Domain.Shared;
using SaBooBo.Domain.Shared.Utils;
using SaBooBo.OrderService.Clients;

namespace SaBooBo.NotificationService.Domain.AggregatesModel;

public class ChannelConfig : AggregateRoot
{
    public Guid MerchantId { get; private set; }

    public Guid ChannelId { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public IDictionary<string, string> Metadata { get; private set; } = null!;

    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public ChannelConfig(Guid merchantId, Guid channelId, string name, string? description, IDictionary<string, string> metadata)
    {
        MerchantId = merchantId;
        ChannelId = channelId;
        Name = name;
        Description = description;
        Metadata = metadata;
    }

    public static ChannelConfig Create(Guid merchantId, Guid channelId, string name, string? description, IDictionary<string, string> metadata)
    {
        return new ChannelConfig(merchantId, channelId, name, description, metadata);
    }

    public void Update(string name, string description, IDictionary<string, string> metadata)
    {
        Name = name;
        Description = description;
        Metadata = metadata;
        UpdatedAt = DateTime.UtcNow.ToUniversalTime();
    }

    public void UpdateMetadata(IDictionary<string, string> metadata)
    {
        if (metadata != null)
        {
            // Update key if exists, otherwise add new key
            foreach (var (key, value) in metadata)
            {
                if (Metadata.ContainsKey(key))
                {
                    Metadata[key] = value;
                }
                else
                {
                    Metadata.Add(key, value);
                }
            }

            UpdatedAt = DateTime.UtcNow.ToUniversalTime();
        }
    }

    /// <summary>
    /// Update Zalo OAuth config
    /// </summary>
    /// <param name="accessToken">The access token</param>
    /// <param name="refreshToken">The refresh token</param>
    /// <param name="expiresIn">The expiration time in seconds</param>
    public void UpdateZaloOAuthConfig(
        string accessToken,
        string refreshToken,
        string expiresIn,
        string? refreshTokenExpiresIn
    )
    {
        Metadata[ZaloKeysConfig.AccessToken] = accessToken;
        Metadata[ZaloKeysConfig.RefreshToken] = refreshToken;
        Metadata[ZaloKeysConfig.ExpiresIn] = DateTime.UtcNow.AddSeconds(
            double.Parse(expiresIn)
        ).ToString();

        // Update refresh token expiration time if exists
        if (string.IsNullOrEmpty(refreshTokenExpiresIn) == false)
        {
            Metadata[ZaloKeysConfig.RefreshTokenExpiresIn] = DateTime.UtcNow.AddSeconds(
                double.Parse(refreshTokenExpiresIn)
            ).ToString();
        }
        
        UpdatedAt = DateTime.UtcNow.ToUniversalTime();
    }

    /// <summary>
    /// Check if the access token is still available. It's mean the access token is not expired yet.
    /// </summary>
    /// <returns>Returns true if the access token is still available, otherwise returns false.</returns>
    public bool IsAccessTokenAvailable()
    {
        if ( Metadata == null || Metadata.ContainsKey(ZaloKeysConfig.AccessToken) == false || Metadata.ContainsKey(ZaloKeysConfig.ExpiresIn) == false )
        {
            return false;
        }

        var expiresAtAccessToken = DateTime.Parse(Metadata[ZaloKeysConfig.ExpiresIn]);

        LoggingUtil.WriteLog($"Expires at access token: {expiresAtAccessToken}", nameof(IsAccessTokenAvailable));
        LoggingUtil.WriteLog($"Current time: {DateTime.UtcNow}", nameof(IsAccessTokenAvailable));
        LoggingUtil.WriteLog($"Is access token available: {DateTime.UtcNow < expiresAtAccessToken}", nameof(IsAccessTokenAvailable));
    

        return DateTime.UtcNow < expiresAtAccessToken;
        
    }
}