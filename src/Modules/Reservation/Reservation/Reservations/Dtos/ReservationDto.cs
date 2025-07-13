using Reservation.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Dtos
{
    public class ReservationDto
    {

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public decimal RoomPrice { get; set; }
        public string RoomDescription { get; set; }
        public int RoomCapacity { get; set; }

        public bool IsConfirmed { get; set; } = false;
        public int NumberDays { get; set; }
        public double TotalPrice { get; set; }

    }
}
