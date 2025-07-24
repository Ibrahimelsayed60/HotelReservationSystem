using Microsoft.EntityFrameworkCore;
using Payment.Data;
using Payment.Payments.Service;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Payments.Features.RetryPaymentSession
{

    public record RetryPaymentSessionCommand(Guid ReservationId): ICommand<RetryPaymentSessionResult>;

    public record RetryPaymentSessionResult(string result);

    public class RetryPaymentSessionHandler(PaymentDbContext dbContext, IPaymobService paymobService) : ICommandHandler<RetryPaymentSessionCommand, RetryPaymentSessionResult>
    {
        public async Task<RetryPaymentSessionResult> Handle(RetryPaymentSessionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await dbContext.PaymentTransactions
            .Where(t => t.ReservationId == request.ReservationId && t.Status != "Paid")
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

            if (transaction == null)
                return new RetryPaymentSessionResult("No failed or pending transaction found for this reservation.");

            // Retry by creating a new session using existing transaction info
            var result = await paymobService.CreatePaymentSessionAsync(transaction);

            if (!string.IsNullOrEmpty( result))
                return new RetryPaymentSessionResult("Failed to create a new Paymob session.");

            return new RetryPaymentSessionResult(result); // iframe URL
        }
    }
}
