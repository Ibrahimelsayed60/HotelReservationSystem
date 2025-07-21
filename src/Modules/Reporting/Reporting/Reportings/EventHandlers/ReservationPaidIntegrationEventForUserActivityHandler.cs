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
    public class ReservationPaidIntegrationEventForUserActivityHandler(ReportingDbContext dbContext, ILogger<ReservationPaidIntegrationEventForUserActivityHandler> logger)
        : IConsumer<ReservationPaidIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ReservationPaidIntegrationEvent> context)
        {
            var msg = context.Message;
            var userId = msg.UserId;

            var report = await dbContext.UserActivityReports
                .FirstOrDefaultAsync(r => r.UserId == userId);

            if (report == null)
            {
                report = new UserActivityReport
                {
                    UserId = userId,
                    TotalReservations = 1,
                    TotalSpend = msg.TotalAmount,
                    LastReservationDate = msg.PaidAt
                };
                dbContext.UserActivityReports.Add(report);
            }
            else
            {
                report.TotalReservations += 1;
                report.TotalSpend += msg.TotalAmount;
                report.LastReservationDate = msg.PaidAt;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
