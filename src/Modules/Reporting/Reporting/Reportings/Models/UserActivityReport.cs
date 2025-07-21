using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Models
{
    public class UserActivityReport: Entity<Guid>
    {
        public Guid UserId { get; set; }
        public int TotalReservations { get; set; }
        public decimal TotalSpend { get; set; }
        public DateTime LastReservationDate { get; set; }

    }
}
