using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Payments.Models
{
    public class RefundTransaction:Entity<Guid>
    {
        public Guid OriginalTransactionId { get; set; }

        public PaymentTransaction OriginalTransaction { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime RefundedAt { get; set; }
        public string? Reason { get; set; }

    }
}
