using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Payments.Events
{
    public record PaymentRefundedEvent(Guid TransactionId) :IDomainEvent;
    
}
