
namespace SaBooBo.MerchantService.Domain.AggregatesModel;

/// <summary>
/// This class is used to store Zalo app setting information.
/// </summary>
public class ZaloAppSetting
{
    /// <summary>
    /// The app id of Zalo mini app.
    /// </summary>
    public string AppId { get; private set; } = null!;

    /// <summary>
    /// The private key of Zalo mini app.
    /// </summary>
    public string PrivateKey { get; private set; }

    /// <summary>
    /// The secret key of Zalo account developer.
    /// </summary>
    public string SecretKey { get; private set; }


    /// <summary>
    /// Constructor of ZaloAppSetting.
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="secretKey"></param>
    /// <param name="privateKey"></param>
    public ZaloAppSetting(string appId, string secretKey, string privateKey)
    {
        AppId = appId;
        SecretKey = secretKey;
        PrivateKey = privateKey;
    }
}