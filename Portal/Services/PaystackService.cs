using Gibs.Portal.Domain.Entities;
using System.Net.Http.Headers;
using System.Security;
using System.Text.Json;

namespace Gibs.Portal.Services
{
    public class PaystackService(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        private readonly IHttpClientFactory _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        private readonly string _testSecretKey = configuration["Paystack:TestSecretKey"] ?? throw new ArgumentNullException(nameof(configuration));

        public async Task<TransactionInitResponse> InitializeTransaction(string email, decimal totalAmount, string callbackUrl)
        {
            var amountInKobo = (int)(totalAmount * 100);
            var reference = Guid.NewGuid().ToString();

            var initRequest = new TransactionInitRequest(email, amountInKobo, callbackUrl, reference);

            var client = CreateHttpClient();
            var url = "https://api.paystack.co/transaction/initialize";
            var response = await client.PostAsJsonAsync(url, initRequest).ConfigureAwait(false);
            var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<PaystackResponse<TransactionInitResponse>>(jsonString);

            if (response.IsSuccessStatusCode)
            {
                if (result != null)
                    return result.Data;

                throw new Exception(jsonString);
            }

            if (result != null)
                throw new Exception(result.Message);

            throw new Exception(jsonString);
        }

        public async Task<TransactionVerifyResponse> VerifyTransaction(string reference)
        {
            var url = $"https://api.paystack.co/transaction/verify/{reference}";

            var client = CreateHttpClient();
            var response = await client.GetAsync(url).ConfigureAwait(false);
            var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<PaystackResponse<TransactionVerifyResponse>>(jsonString);

            if (response.IsSuccessStatusCode)
            {
                if (result != null)
                    return result.Data;

                throw new VerificationException(jsonString);
            }

            if (result != null)
                throw new VerificationException(result.Message);

            throw new VerificationException(jsonString);
        }

        private HttpClient CreateHttpClient()
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _testSecretKey);
            return client;
        }
    }

}
