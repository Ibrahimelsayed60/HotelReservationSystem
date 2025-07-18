using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Models
{
    public class OfferUsage:Entity<Guid>
    {
        public Guid ReservationId { get; set; }
        public Guid OfferId { get; set; }

        public Offer Offer { get; set; }
        public DateTime AppliedAt { get; set; }
        public decimal DiscountAmount { get; set; }

    }
}
