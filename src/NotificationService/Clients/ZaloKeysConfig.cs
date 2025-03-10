
namespace SaBooBo.OrderService.Clients;

/// <summary>
/// This class contains the keys for the Zalo API configuration.
/// </summary>
public class ZaloKeysConfig
{
    /// <summary>
    /// The app ID key.
    /// You can visit https://developers.zalo.me/app/{appId}/settings to get the app ID.
    /// </summary>
    public const string AppId = "app_id";

    /// <summary>
    /// The OAuth code key.
    /// This code is recieved from callback URL after the user has authorized the app.
    /// You can visit hhttps://developers.zalo.me/app/{{appId}}/oa/settings to get the app ID.
    /// </summary>
    public const string OAuthCode = "oauth_code";

    /// <summary>
    /// This is the grant type for the Zalo API.
    /// </summary>
    public const string GrantTypeAuthorization = "authorization_code";

    /// <summary>
    /// The secret key key.
    /// You can visit https://developers.zalo.me/app/{appId}/settings to get the secret key.
    /// </summary>
    public const string SecretKey = "secret_key";

    /// <summary>
    /// The secret key key.
    /// You can visit https://mini.zalo.me/miniapp/{appId}/payment/settings to get the private key.
    /// </summary>
    public const string PrivateKey = "private_key";

    /// <summary>
    /// The grant type key. The grant type is always authorization_code.
    /// </summary>
    public const string GrantType = "grant_type";

    /// <summary>
    /// The access token key. This is the key to authenticate the user with Zalo.
    /// The maximum expiration time of the access token is 1 hour = 3600 seconds.
    /// </summary>
    public const string AccessToken = "access_token";

    /// <summary>
    /// The refresh token key. This is the key to refresh the access token.
    /// The maximum expiration time of the access token is 30 days = 720 hours = 2592000 seconds.
    /// </summary>
    public const string RefreshToken = "refresh_token";

    /// <summary>
    /// The expires in key. This is the key to get the expiration time of the access token.
    /// The expiration time is in seconds.
    /// </summary>
    public const string ExpiresIn = "expires_in";

    /// <summary>
    /// This is the key to get the expiration time of the refresh token.
    /// The expiration time is in seconds.
    /// </summary>
    public const string RefreshTokenExpiresIn = "refresh_token_expires_in";

}