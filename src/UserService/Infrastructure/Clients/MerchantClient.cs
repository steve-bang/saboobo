
using SaBooBo.Domain.Shared.Clients;
using SaBooBo.UserService.Application.Clients;

namespace SaBooBo.UserService.Infrastructure.Clients
{
    public class MerchantClient : IMerchantClient
    {
        private readonly IHttpClientFactory _factory;

        public MerchantClient(IHttpClientFactory httpClientFactory)
        {
            _factory = httpClientFactory;
        }

        public async Task<object?> GetMerchantByIdAsync(Guid merchantId)
        {
            using var _httpClient = _factory.CreateClient(ClientNames.Saboobo);

            try {
                var response = await _httpClient.GetAsync($"/api/v1/merchants/{merchantId}");

                if (!response.IsSuccessStatusCode) {
                    return null;
                }
                return response.Content;

            } catch (Exception e) {
                Console.WriteLine(e);

                return null;
            }
        }
    }
}