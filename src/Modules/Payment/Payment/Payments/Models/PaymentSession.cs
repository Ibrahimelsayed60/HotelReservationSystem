using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Payments.Models
{
    public class PaymentSession:Entity<Guid>
    {
        public Guid TransactionId { get; set; }
        public PaymentTransaction Transaction { get; set; }
        public string PaymentUrl { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
