using Microsoft.Extensions.Configuration;
using Payment.Payments.Models;
using X.Paymob.CashIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Payment.Payments.Service
{
    public class PaymobService : IPaymobService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymobService> _logger;

        private readonly string? _apiKey;
        private readonly string? _secretKey;
        private readonly string? _integrationIdCard;
        private readonly string? _iframeId;

        public PaymobService(HttpClient httpClient, IConfiguration configuration, ILogger<PaymobService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;

            _apiKey = _configuration["Paymob:APIKey"];
            _secretKey = _configuration["Paymob:SecretKey"];
            _integrationIdCard = _configuration["Paymob:IntegrationIdCard"];
            _iframeId = _configuration["Paymob:IframeId"];
        }

        public async Task<string> CreatePaymentSessionAsync(PaymentTransaction transaction)
        {
            try
            {
                // Step 1: Auth token
                var tokenRequest = new { api_key = _secretKey };
                var tokenResponse = await _httpClient.PostAsync(
                    "https://accept.paymob.com/api/auth/tokens",
                    new StringContent(JsonSerializer.Serialize(tokenRequest), Encoding.UTF8, "application/json"));

                var tokenJson = await tokenResponse.Content.ReadAsStringAsync();
                var authToken = JsonDocument.Parse(tokenJson).RootElement.GetProperty("token").GetString();

                // Step 2: Create order
                var orderPayload = new
                {
                    auth_token = authToken,
                    delivery_needed = false,
                    amount_cents = (int)(transaction.Amount * 100),
                    currency = "EGP",
                    items = new object[] { }
                };

                var orderResponse = await _httpClient.PostAsync(
                    "https://accept.paymob.com/api/ecommerce/orders",
                    new StringContent(JsonSerializer.Serialize(orderPayload), Encoding.UTF8, "application/json"));

                var orderJson = await orderResponse.Content.ReadAsStringAsync();
                var orderId = JsonDocument.Parse(orderJson).RootElement.GetProperty("id").GetInt32();

                // Step 3: Create payment key
                var billingData = new
                {
                    apartment = "NA",
                    email = "user@example.com",
                    floor = "NA",
                    first_name = "Hotel",
                    street = "NA",
                    building = "NA",
                    phone_number = "+201000000000",
                    shipping_method = "NA",
                    postal_code = "NA",
                    city = "Cairo",
                    country = "EG",
                    last_name = "Customer",
                    state = "NA"
                };

                var paymentKeyPayload = new
                {
                    auth_token = authToken,
                    amount_cents = (int)(transaction.Amount * 100),
                    expiration = 3600,
                    order_id = orderId,
                    billing_data = billingData,
                    currency = "EGP",
                    integration_id = int.Parse(_integrationIdCard ?? "0")
                };

                var keyResponse = await _httpClient.PostAsync(
                    "https://accept.paymob.com/api/acceptance/payment_keys",
                    new StringContent(JsonSerializer.Serialize(paymentKeyPayload), Encoding.UTF8, "application/json"));

                var keyJson = await keyResponse.Content.ReadAsStringAsync();
                var paymentToken = JsonDocument.Parse(keyJson).RootElement.GetProperty("token").GetString();

                // Step 4: Build iframe URL
                var iframeUrl = $"https://accept.paymob.com/api/acceptance/iframes/{_iframeId}?payment_token={paymentToken}";

                return iframeUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Paymob payment session.");
                return "Failed to initiate payment session.";
            }
        }
    }

}
