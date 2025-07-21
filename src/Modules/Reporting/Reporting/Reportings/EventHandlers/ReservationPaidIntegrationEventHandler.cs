using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reporting.Data;
using Reporting.Reportings.Models;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.EventHandlers
{
    public class ReservationPaidIntegrationEventHandler(ReportingDbContext dbContext, ILogger<ReservationPaidIntegrationEventHandler> logger) : IConsumer<ReservationPaidIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ReservationPaidIntegrationEvent> context)
        {
            var msg = context.Message;
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var record = await dbContext.DailyRevenueReports
                .FirstOrDefaultAsync(x => x.Date == today);

            if (record == null)
            {
                // Insert new day
                record = new DailyRevenueReport
                {
                    Date = today,
                    ReservationsCount = 1,
                    TotalRevenue = msg.TotalAmount
                };
                dbContext.DailyRevenueReports.Add(record);
            }
            else
            {
                // Update existing day
                record.ReservationsCount += 1;
                record.TotalRevenue += msg.TotalAmount;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
