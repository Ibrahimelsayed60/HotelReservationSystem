using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Payments.Models
{
    public class PaymentWebhookLog:Entity<Guid>
    {
        public string Provider { get; set; }
        public string EventType { get; set; }
        public string RawPayload { get; set; }
        public DateTime ReceivedAt { get; set; }
    }
}
