
namespace SaBooBo.MerchantService.Domain.AggregatesModel;

/// <summary>
/// This class is used to store Zalo app setting information.
/// </summary>
public class ZaloAppSetting
{
    /// <summary>
    /// The OA id of Zalo page.
    /// </summary>
    public string? OAId { get; private set; }


    /// <summary>
    /// The app id of zalo account developer.
    /// </summary>
    public string? AppId { get; private set; }

    /// <summary>
    /// The private key of Zalo mini app.
    /// See more: https://mini.zalo.me/miniapp/{miniappId}/payment/settings
    /// </summary>
    public string? AppPrivateKey { get; private set; }

    /// <summary>
    /// The oauth code of Zalo account developer.
    /// This code is used to get access token.
    /// See more: https://developers.zalo.me/app/{id}/oa/settings
    /// </summary>
    public string? AppOauthCode { get; private set; }

    /// <summary>
    /// The id of Zalo mini app.
    /// </summary>
    public string? MiniAppId { get; private set; }

    /// <summary>
    /// The secret key of Zalo account developer.
    /// See more: https://developers.zalo.me/app/{id}/oa/settings
    /// </summary>
    public string? MiniAppSecretKey { get; private set; }


    /// <summary>
    /// Create a new instance of ZaloAppSetting.
    /// </summary>
    /// <param name="oaId">The OA id of Zalo page.</param>
    /// <param name="appId">The app id of zalo account developer.</param>
    /// <param name="appPrivateKey">The private key of Zalo mini app.</param>
    /// <param name="appOauthCode">The oauth code of Zalo account developer.</param>
    /// <param name="miniAppId">The id of Zalo mini app.</param>
    /// <param name="miniAppSecretKey">The secret key of Zalo account developer.</param>
    public ZaloAppSetting(string oaId, string appId, string appPrivateKey, string appOauthCode, string miniAppId, string miniAppSecretKey)
    {
        OAId = oaId;
        AppId = appId;
        AppPrivateKey = appPrivateKey;
        AppOauthCode = appOauthCode;
        MiniAppId = miniAppId;
        MiniAppSecretKey = miniAppSecretKey;
    }
}