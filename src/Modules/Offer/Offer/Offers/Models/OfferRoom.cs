using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Models
{
    public class OfferRoom
    {

        public Guid OfferId { get; set; }
        public Offer Offer { get; set; }

        public Guid RoomId { get; set; }
        // Room is in another module, you can reference just the RoomId here

        public decimal? CustomDiscountPercentage { get; set; } // Optional: room-specific discount

    }
}
