using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Models
{
    public enum ReservationStatus
    {

        Pending,
        Confirmed,
        CheckedIn,
        CheckedOut,
        Cancelled,
        Completed

    }
}
