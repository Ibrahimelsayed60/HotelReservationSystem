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
    public class ReservationCreatedIntegrationEventHandler(ReportingDbContext dbContext, ILogger<ReservationCreatedIntegrationEventHandler> logger) : IConsumer<ReservationCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ReservationCreatedIntegrationEvent> context)
        {
            var msg = context.Message;

            var report = new ReservationReport
            {
                ReservationId = msg.ReservationId,
                UserId = msg.UserId,
                RoomId = msg.RoomId,
                CheckInDate = msg.CheckInDate,
                CheckOutDate = msg.CheckOutDate,
                BasePrice = msg.BasePrice,
                Status = "Created",
                CreatedAt = msg.CreatedAt
            };

            dbContext.ReservationReports.Add(report);
            await dbContext.SaveChangesAsync();
        }
    }
}
