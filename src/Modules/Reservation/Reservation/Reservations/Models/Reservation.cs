using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Models
{
    public class Reservation : Aggregate<Guid>
    {

        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public bool IsConfirmed { get; set; } = false;
        public int NumberDays { get; set; }
        public double TotalPrice { get; set; }

    }
}
