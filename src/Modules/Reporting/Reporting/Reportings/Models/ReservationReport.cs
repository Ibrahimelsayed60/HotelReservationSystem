using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Models
{
    public class ReservationReport: Entity<Guid>
    {
        public Guid ReservationId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal BasePrice { get; set; }
        public decimal? DiscountApplied { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } // e.g., Booked, Paid, Cancelled, CheckedOut
        public DateTime CreatedAt { get; set; }

    }
}
