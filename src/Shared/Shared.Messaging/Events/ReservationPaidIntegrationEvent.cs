using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging.Events
{
    public record ReservationPaidIntegrationEvent:IntegrationEvent
    {
        public Guid ReservationId { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaidAt { get; set; }

        public Guid? AppliedOfferId { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public string? OfferTitle { get; set; }
    }
}
