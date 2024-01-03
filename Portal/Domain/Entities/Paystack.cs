using System.Text.Json.Serialization;

namespace Gibs.Portal.Domain.Entities
{
    public class PaystackResponse<T>(bool status, string? message, T data) where T : class
    {
        [JsonPropertyName("status")]
        public bool Status { get; } = status;

        [JsonPropertyName("message")]
        public string? Message { get; } = message;

        [JsonPropertyName("data")]
        public T Data { get; } = data;
    }

    public class TransactionInitRequest(string email, int amount, string callbackUrl, string reference)
    {
        [JsonPropertyName("email")]
        public string Email { get; } = email;

        [JsonPropertyName("amount")]
        public int Amount { get; } = amount;

        [JsonPropertyName("callback_url")]
        public string CallbackUrl { get; } = callbackUrl;

        [JsonPropertyName("reference")]
        public string Reference { get; } = reference;
    }

    public class TransactionInitResponse(string authorizationUrl, string accessCode, string reference)
    {
        [JsonPropertyName("authorization_url")]
        public string AuthorizationUrl { get; } = authorizationUrl;

        [JsonPropertyName("access_code")]
        public string AccessCode { get; } = accessCode;

        [JsonPropertyName("reference")]
        public string Reference { get; } = reference;
    }

    public class TransactionVerifyResponse(decimal amountInKobo, string reference, string status, string message, string gatewayResponse, decimal requestedAmount, CustomerData customer)
    {
        [JsonPropertyName("amount")]
        public decimal AmountInKobo { get; } = amountInKobo;


        [JsonPropertyName("reference")]
        public string Reference { get; } = reference;


        [JsonPropertyName("status")]
        public string Status { get; } = status;


        [JsonPropertyName("message")]
        public string Message { get; } = message;


        [JsonPropertyName("gateway_response")]
        public string GatewayResponse { get; } = gatewayResponse;


        [JsonPropertyName("requested_amount")]
        public decimal RequestedAmount { get; } = requestedAmount;

        [JsonPropertyName("customer")]
        public CustomerData Customer { get; } = customer;
    }

    public class CustomerData(string firstName, string lastName, string email)
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; } = firstName;

        [JsonPropertyName("last_name")]
        public string LastName { get; } = lastName;

        [JsonPropertyName("email")]
        public string Email { get; } = email;
    }
}
