using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Models
{
    public class DailyRevenueReport
    {
        public DateOnly Date { get; set; }
        public int ReservationsCount { get; set; }
        public decimal TotalRevenue { get; set; }

    }
}
