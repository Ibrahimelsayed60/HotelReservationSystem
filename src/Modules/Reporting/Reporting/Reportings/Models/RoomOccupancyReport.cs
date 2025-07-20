using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Models
{
    public class RoomOccupancyReport
    {
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public int TotalNightsBooked { get; set; }
        public int TotalReservations { get; set; }
        public decimal TotalRevenue { get; set; }

    }
}
