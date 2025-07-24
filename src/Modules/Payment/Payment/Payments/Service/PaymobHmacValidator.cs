using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Payment.Payments.Service
{
    public class PaymobHmacValidator : IPaymobHmacValidator
    {
        private readonly IConfiguration _configuration;

        public PaymobHmacValidator(IConfiguration configuration )
        {
            _configuration = configuration;
        }

        public bool IsValid(JsonElement obj, string receivedHmac)
        {
            var hmacSecret = _configuration["Paymob:Hmac"];
            if (string.IsNullOrWhiteSpace(hmacSecret))
                return false;

            var fields = new[]
            {
                "amount_cents", "created_at", "currency", "error_occured", "has_parent_transaction",
                "id", "integration_id", "is_3d_secure", "is_auth", "is_capture", "is_refunded",
                "is_standalone_payment", "is_voided", "order.id", "owner", "pending",
                "source_data.pan", "source_data.sub_type", "source_data.type", "success"
            };

            var values = new List<string>();

            foreach (var field in fields)
            {
                string[] path = field.Split('.');
                JsonElement current = obj;

                foreach (var part in path)
                {
                    if (!current.TryGetProperty(part, out current))
                    {
                        values.Add("");
                        goto NextField;
                    }
                }

                var value = current.ToString();
                if (value == "True" || value == "False")
                    value = value.ToLower();

                values.Add(value);

            NextField:;
            }

            string concatenated = string.Concat(values);

            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(hmacSecret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(concatenated));
            var computedHmac = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

            return computedHmac == receivedHmac.ToLowerInvariant();
        }
    }
}
