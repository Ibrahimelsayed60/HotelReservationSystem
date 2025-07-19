using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Dtos
{
    public class CachedOffer
    {
        public Guid OfferId { get; set; }
        public string Title { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Guid> RoomIds { get; set; } = new(); // optional: which rooms it applies to

    }
}
