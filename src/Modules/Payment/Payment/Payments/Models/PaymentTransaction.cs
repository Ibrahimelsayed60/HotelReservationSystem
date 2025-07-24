using Payment.Payments.Events;
using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Payments.Models
{
    public class PaymentTransaction:Aggregate<Guid>
    {
        public Guid ReservationId { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        public string Status { get; private set; } // e.g., Pending, Paid
        public string PaymentMethod { get; private set; }
        public string Provider { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? PaidAt { get; private set; }
        public string? ProviderTransactionId { get; private set; }
        public string? FailureReason { get; private set; }

        public void MarkAsPaid(string providerTransactionId)
        {
            Status = "Paid";
            PaidAt = DateTime.UtcNow;
            ProviderTransactionId = providerTransactionId;
            AddDomainEvent(new PaymentSucceededEvent(Id));
        }

        public void MarkAsFailed(string reason)
        {
            Status = "Failed";
            FailureReason = reason;
            AddDomainEvent(new PaymentFailedEvent(Id, reason));
        }

    }
}
