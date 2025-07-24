using Payment.Data;
using Payment.Payments.Models;
using Payment.Payments.Service;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Payments.Features.InitiatePayment
{
    public record InitiatePaymentCommand(
    Guid ReservationId,
    Guid UserId,
    decimal Amount
    ):ICommand<InitiatePaymentResult>;

    public record InitiatePaymentResult(string frame);

    public class InitiatePaymentHandler(IPaymobService paymobService, PaymentDbContext dbContext) : ICommandHandler<InitiatePaymentCommand, InitiatePaymentResult>
    {
        public async Task<InitiatePaymentResult> Handle(InitiatePaymentCommand request, CancellationToken cancellationToken)
        {
            // Check for existing unpaid transaction (optional)
            var existingTransaction = dbContext.PaymentTransactions
                .FirstOrDefault(t => t.ReservationId == request.ReservationId && t.Status == "Pending");

            if (existingTransaction != null)
            {
                return new InitiatePaymentResult( "https://accept.paymob.com/api/acceptance/iframes/" +
                                       "{iframe_id}?payment_token=" + existingTransaction.ProviderTransactionId);
            }

            // Create a new payment transaction
            var transaction = new PaymentTransaction(request.ReservationId, request.UserId, request.Amount);
            dbContext.PaymentTransactions.Add(transaction);
            await dbContext.SaveChangesAsync(cancellationToken);

            // Create payment session using Paymob
            var result = await paymobService.CreatePaymentSessionAsync(transaction);
            if (!string.IsNullOrEmpty(result))
                return new InitiatePaymentResult("Failed to generate payment session.");

            return new InitiatePaymentResult( result); // payment URL
        }
    }
}
