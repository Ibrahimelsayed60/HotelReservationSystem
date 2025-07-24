using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payment.Data;
using Payment.Payments.Service;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Payment.Payments.Features.HandlePaymobWebhook
{

    public record HandlePaymobWebhookCommand(string Payload, string Hmac) :ICommand<HandlePaymobWebhookResult>;

    public record HandlePaymobWebhookResult(string result);

    public class HandlePaymobWebhookHandler(IPaymobHmacValidator _hmacValidator, ILogger<HandlePaymobWebhookHandler> _logger, PaymentDbContext _context) : ICommandHandler<HandlePaymobWebhookCommand, HandlePaymobWebhookResult>
    {
        public async Task<HandlePaymobWebhookResult> Handle(HandlePaymobWebhookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var root = JsonDocument.Parse(request.Payload).RootElement;
                var obj = root.GetProperty("obj");

                // Optional: Validate HMAC signature
                if (!_hmacValidator.IsValid(obj, request.Hmac))
                {
                    _logger.LogWarning("Invalid HMAC received from Paymob webhook.");
                    return new HandlePaymobWebhookResult("Invalid HMAC");
                }

                var success = obj.GetProperty("success").GetBoolean();
                var providerTransactionId = obj.GetProperty("id").ToString();
                var extra = obj.GetProperty("payment_key_claims").GetProperty("extra");
                var reservationId = Guid.Parse(extra.GetProperty("reservationId").GetString()!);

                var transaction = await _context.PaymentTransactions
                    .FirstOrDefaultAsync(t => t.ReservationId == reservationId, cancellationToken);

                if (transaction == null)
                    return new HandlePaymobWebhookResult("Transaction not found");

                if (success)
                    transaction.MarkAsPaid(providerTransactionId);
                else
                    transaction.MarkAsFailed("Paymob reported payment failure");

                await _context.SaveChangesAsync(cancellationToken);

                return new HandlePaymobWebhookResult("Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Paymob webhook");
                return new HandlePaymobWebhookResult("Webhook processing failed");
            }
        }
    }
}
