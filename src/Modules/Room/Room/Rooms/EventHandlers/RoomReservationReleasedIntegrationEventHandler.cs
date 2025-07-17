using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Room.Data;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.EventHandlers
{
    public class RoomReservationReleasedIntegrationEventHandler(RoomDbContext dbContext, ILogger<RoomReservationReleasedIntegrationEventHandler> logger)
        : IConsumer<RoomReservationReleasedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<RoomReservationReleasedIntegrationEvent> context)
        {
            var evt = context.Message;

            var room = await dbContext.Rooms.FindAsync(evt.RoomId);
            if (room is null)
            {
                logger.LogWarning("Room not found for ID {RoomId}", evt.RoomId);
                return;
            }

            // ✅ Mark room as available
            room.IsAvailable = true;

            await dbContext.SaveChangesAsync();

            logger.LogInformation("Room {RoomId} is now marked as available due to reservation release.", evt.RoomId);
        }
    }
}
