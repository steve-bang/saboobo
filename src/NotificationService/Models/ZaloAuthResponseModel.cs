
using System.Text.Json.Serialization;

namespace SaBooBo.NotificationService.Models
{
    public class ZaloAuthResponseModel
    {
        /// <summary>
        /// The access token to authenticate the user with Zalo.
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = null!;

        /// <summary>
        /// The refresh token to refresh the access token.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = null!;

        /// <summary>
        /// The time in seconds until the access token expires.
        /// The unit is in seconds.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public string ExpiresIn { get; set; } = null!;

        /// <summary>
        /// This is the time in seconds until the refresh token expires.
        /// The unit is in seconds.
        /// </summary>
        [JsonPropertyName("refresh_token_expires_in")]
        public string? RefreshTokenExpiresIn { get; set; }

        /// <summary>
        /// Returns true if the response is an error from calling Zalo API.
        /// </summary>
        /// <returns></returns>
        public bool IsError()
        {
            return AccessToken == null || RefreshToken == null || ExpiresIn == null;
        }
    }
}