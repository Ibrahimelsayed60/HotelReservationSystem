using Microsoft.EntityFrameworkCore;
using Payment.Data;
using Payment.Payments.Dtos;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Payments.Features.GetTransactionStatus
{
    public record GetTransactionStatusQuery(Guid ReservationId):IQuery<GetTransactionStatusResult>;

    public record GetTransactionStatusResult(TransactionStatusDto TransactionStatus);

    public class GetTransactionStatusHandler(PaymentDbContext dbContext) : IQueryHandler<GetTransactionStatusQuery, GetTransactionStatusResult>
    {
        public async Task<GetTransactionStatusResult> Handle(GetTransactionStatusQuery request, CancellationToken cancellationToken)
        {
            var transaction = await dbContext.PaymentTransactions
            .Where(t => t.ReservationId == request.ReservationId)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

            if (transaction == null)
                throw new Exception("Transaction not found.");

            var dto = new TransactionStatusDto
            {
                Status = transaction.Status,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt,
                PaidAt = transaction.PaidAt,
                FailureReason = transaction.FailureReason,
                ProviderTransactionId = transaction.ProviderTransactionId
            };

            return new GetTransactionStatusResult(dto);
        }
    }
}
