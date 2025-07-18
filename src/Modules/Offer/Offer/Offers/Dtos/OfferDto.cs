using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Dtos
{
    public class OfferDto
    {

        public Guid Id { get; set; }
        public string Title { get; set; }                     // e.g. "Summer Sale"
        public string Description { get; set; }               // e.g. "Get 20% off..."
        public decimal DiscountPercentage { get; set; }       // e.g. 20.0
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
