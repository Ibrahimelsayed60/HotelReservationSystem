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
    public class ReservationCheckedOutIntegrationEventHandler(ReportingDbContext dbContext, ILogger<ReservationCheckedOutIntegrationEventHandler> logger)
        : IConsumer<ReservationCheckedOutIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ReservationCheckedOutIntegrationEvent> context)
        {
            var msg = context.Message;

            int nights = (msg.CheckOutDate - msg.CheckInDate).Days;

            var report = await dbContext.RoomOccupancyReports
                .FirstOrDefaultAsync(r => r.RoomId == msg.RoomId);

            if (report == null)
            {
                report = new RoomOccupancyReport
                {
                    RoomId = msg.RoomId,
                    RoomName = msg.RoomName,
                    TotalNightsBooked = nights,
                    TotalReservations = 1,
                    TotalRevenue = msg.TotalAmount
                };

                dbContext.RoomOccupancyReports.Add(report);
            }
            else
            {
                report.TotalNightsBooked += nights;
                report.TotalReservations += 1;
                report.TotalRevenue += msg.TotalAmount;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
