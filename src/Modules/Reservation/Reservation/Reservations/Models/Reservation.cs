using Reservation.Reservations.Events;
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

        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public decimal RoomPrice { get; set; }
        public string RoomDescription { get; set; }
        public int RoomCapacity { get; set; }

        public bool IsConfirmed { get; set; } = false;
        public int NumberDays { get; set; }
        public double TotalPrice { get; set; }


        public static Reservation Create(Guid userId, Guid roomId, DateTime checkIn, DateTime checkOut,
                                     string name, string type, decimal price)
        {
            if (checkIn >= checkOut)
                throw new Exception("Check-out must be after check-in");

            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                RoomId = roomId,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                RoomName = name,
                RoomType = type,
                RoomPrice = price,
                CreatedAt = DateTime.UtcNow,
                Status = ReservationStatus.Pending
            };

            reservation.AddDomainEvent(new ReservationCreatedEvent(reservation));
            return reservation;
        }

        public void Cancel()
        {
            if (Status == ReservationStatus.Cancelled)
                throw new Exception("Already cancelled.");

            Status = ReservationStatus.Cancelled;

            AddDomainEvent(new ReservationCancelledEvent(Id, UserId, RoomId, CheckInDate, CheckOutDate));
        }

        public void MarkAsPaid(decimal amount, string method)
        {
            if (Status != ReservationStatus.Pending)
                throw new Exception("Cannot pay a reservation that is not pending.");

            Status = ReservationStatus.Confirmed;

            AddDomainEvent(new ReservationPaidEvent(Id, UserId, RoomId, amount, method));
        }

        public void CheckIn()
        {
            if (Status != ReservationStatus.Confirmed)
                throw new Exception("Only confirmed reservations can be checked in.");

            Status = ReservationStatus.CheckedIn;

            AddDomainEvent(new ReservationCheckedInEvent(Id, UserId, RoomId, DateTime.UtcNow));
        }

        public void CheckOut()
        {
            if (Status != ReservationStatus.CheckedIn)
                throw new Exception("Only checked-in reservations can be checked out.");

            Status = ReservationStatus.CheckedOut;

            AddDomainEvent(new ReservationCheckedOutEvent(Id, UserId, RoomId, DateTime.UtcNow));
        }

    }
}
