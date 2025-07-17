using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Models
{
    public class Offer : Aggregate<Guid>
    {
        public string Title { get; set; }                     // e.g. "Summer Sale"
        public string Description { get; set; }               // e.g. "Get 20% off..."
        public decimal DiscountPercentage { get; set; }       // e.g. 20.0
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive => StartDate <= DateTime.UtcNow && EndDate >= DateTime.UtcNow;

        public ICollection<OfferRoom> OfferRooms { get; set; }

    }
}
