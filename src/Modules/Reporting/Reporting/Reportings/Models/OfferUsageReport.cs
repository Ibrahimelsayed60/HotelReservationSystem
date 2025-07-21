using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Models
{
    public class OfferUsageReport: Entity<Guid>
    {
        public Guid OfferId { get; set; }
        public string Title { get; set; }
        public int TimesUsed { get; set; }
        public decimal TotalDiscountGiven { get; set; }

    }
}
