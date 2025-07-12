using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Events
{
    public record ReservationCheckedOutEvent(Guid ReservationId, Guid UserId, Guid RoomId, DateTime CheckedOutAt) : IDomainEvent;
}
