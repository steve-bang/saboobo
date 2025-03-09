
using SaBooBo.NotificationService.Models;

namespace SaBooBo.OrderService.Clients
{
    public interface IZaloClient
    {
        /// <summary>
        /// Get the access token from Zalo.
        /// </summary>
        /// <param name="secretKey">The secret key of the Zalo app.</param>
        /// <param name="oauthCode">The OAuth code from Zalo.</param>
        /// <param name="appId">The app ID of the Zalo app.</param>
        /// <returns></returns>
        Task<ZaloAuthResponseModel?> GetAccessTokenAsync(string secretKey, string oauthCode, string appId); 

        /// <summary>
        /// Get the access token from Zalo using the refresh token.
        /// </summary>
        /// <param name="secretKey">The secret key of the Zalo app.</param>
        /// <param name="refreshToken">The refresh token from Zalo.</param>
        /// <param name="appId">The app ID of the Zalo app.</param>
        /// <returns></returns>
        Task<ZaloAuthResponseModel?> GetAccessTokenFromRefreshTokenAsync(string secretKey, string refreshToken, string appId);

        /// <summary>
        /// The method to send a message to Zalo.
        /// </summary>
        /// <param name="accessToken">The access token to authenticate the user with Zalo.</param>
        /// <param name="request">The request model to send to Zalo.</param>
        /// <returns></returns>
        Task<string?> SendMessageAsync(string accessToken, object request);
        
    }
}