using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using SaBooBo.Domain.Shared.Utils;
using SaBooBo.NotificationService.Models;

namespace SaBooBo.OrderService.Clients
{
    public class ZaloClient : IZaloClient
    {

        private readonly IHttpClientFactory _factory;

        public ZaloClient(IHttpClientFactory httpClientFactory)
        {
            _factory = httpClientFactory;
        }

        public async Task<ZaloAuthResponseModel?> GetAccessTokenAsync(string secretKey, string oauthCode, string appId)
        {
            try
            {
                var client = _factory.CreateClient(ZaloClientNames.ZaloAuthClient);

                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("secret_key", secretKey);

                Console.WriteLine($"Getting access token from Zalo with secret key: {secretKey}, oauth code: {oauthCode}, and app ID: {appId}");

                var response = await client.PostAsync("/v4/oa/access_token", new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "code", oauthCode },
                    { "app_id", appId },
                    { "grant_type", "authorization_code" } // The grant type is always authorization_code.
                }));

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                LoggingUtil.WriteLog($"Response from Api GetAccessTokenAsync: {content}", nameof(ZaloClient));

                return JsonSerializer.Deserialize<ZaloAuthResponseModel>(content);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting access token from Zalo: {e}");

                throw new Exception("Failed to get access token from Zalo.");
            }
        }

        public async Task<ZaloAuthResponseModel?> GetAccessTokenFromRefreshTokenAsync(string secretKey, string refreshToken, string appId)
        {
            try
            {
                var client = _factory.CreateClient(ZaloClientNames.ZaloAuthClient);

                var request = new HttpRequestMessage(HttpMethod.Post, "/v4/oa/access_token");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("secret_key", secretKey);
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "refresh_token", refreshToken },
                    { "app_id", appId },
                    { "grant_type", "refresh_token" } // The grant type is always refresh_token.
                });

                PrintCurlCommand(request);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                LoggingUtil.WriteLog($"Response from Api GetAccessTokenFromRefreshTokenAsync: {content}", nameof(ZaloClient));

                return JsonSerializer.Deserialize<ZaloAuthResponseModel>(content);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting access token from Zalo: {e}");

                throw new Exception("Failed to get access token from Zalo.");
            }
        }

        public async Task<string?> SendMessageAsync(string accessToken, object request)
        {
            try
            {
                var client = _factory.CreateClient(ZaloClientNames.ZaloOpenApiClient);

                LoggingUtil.WriteLog($"Sending message to Zalo with access token: {accessToken}");

                // Serialize the request model with and use Encoding.UTF8
                var json = JsonSerializer.Serialize(request, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                var bodyRequest = new StringContent(json, Encoding.UTF8, "application/json");

                // Create HttpRequestMessage
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/v3.0/oa/message/transaction")
                {
                    Headers =
                    {
                        { "Accept", "application/json" },
                        { "access_token", accessToken }
                    },
                    Content = bodyRequest,
                };

                // Print cURL command
                PrintCurlCommand(httpRequest, json);

                var response = await client.SendAsync(httpRequest);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                LoggingUtil.WriteLog($"Response from Api SendMessageAsync: {content}", nameof(ZaloClient));

                return content;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error sending message to Zalo: " + e);

                throw new Exception("Failed to get access token from Zalo.");
            }
        }

        private void PrintCurlCommand(HttpRequestMessage request)
        {
            var curl = $"curl -X {request.Method} '{request.RequestUri}'";

            foreach (var header in request.Headers)
            {
                curl += $" -H '{header.Key}: {string.Join(", ", header.Value)}'";
            }

            if (request.Content != null)
            {
                var content = request.Content.ReadAsStringAsync().Result;
                curl += $" -d '{content}'";
            }

            LoggingUtil.WriteLog($"CURL command: {curl}", nameof(ZaloClient));
        }

        private void PrintCurlCommand(HttpRequestMessage request, string body)
        {
            var curl = new StringBuilder();
            curl.Append("curl -X ")
                .Append(request.Method)
                .Append(" \"")
                .Append(request.RequestUri)
                .Append("\"");

            foreach (var header in request.Headers)
            {
                curl.Append(" -H \"")
                    .Append(header.Key)
                    .Append(": ")
                    .Append(string.Join(", ", header.Value))
                    .Append("\"");
            }

            if (request.Content != null)
            {
                curl.Append(" -H \"Content-Type: application/json\"");
                curl.Append(" -d '").Append(body).Append("'");
            }

            Console.WriteLine("cURL Request:");
            Console.WriteLine(curl.ToString());
        }

        /// <inheritdoc/>
        public async Task UpdateOrderStatusCODAsync(string miniAppId, string orderId, string privateKey, ResultCodes resultCode)
        {
            string endpoint = $"https://payment-mini.zalo.me/api/transaction/{miniAppId}/cod-callback-payment";

            string data = $"appId={miniAppId}&orderId={orderId}&resultCode={(short)resultCode}&privateKey={privateKey}";

            string hmac = HashHMACSha256(data, privateKey);

            LoggingUtil.WriteLog($"Updating order status with endpoint: {endpoint}", nameof(ZaloClient));
            LoggingUtil.WriteLog($"Updating order status with data: {data}", nameof(ZaloClient));
            LoggingUtil.WriteLog($"Updating order status with hmac: {hmac}", nameof(ZaloClient));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);

                client.DefaultRequestHeaders.Add("Content-Type", "application/json");

                var payloadData = new
                {
                    appId = miniAppId,
                    orderId = orderId,
                    resultCode = (short)resultCode,
                    hmac = hmac
                };

                var json = JsonSerializer.Serialize(payloadData);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(endpoint, content);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                LoggingUtil.WriteLog($"Response from Api UpdateOrderStatusAsync: {responseContent}", nameof(ZaloClient));

            }
        }


        /// <summary>
        /// Hash HMAC SHA256
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <param name="privateKey">The private key to hash</param>
        /// <returns></returns>
        private string HashHMACSha256(string data, string privateKey)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(privateKey)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                string mac = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return mac;
            }
        }
    }

    /// <summary>
    /// This enum represents the status of an order.
    /// See <see href="https://mini.zalo.me/documents/checkout-sdk/updateOrderStatus/#parameters">updateOrderStatus</see> for more information.
    /// </summary>
    public enum ResultCodes
    {
        Failed = -1,
        Refunded = 0,
        Success = 1,
    } 
}