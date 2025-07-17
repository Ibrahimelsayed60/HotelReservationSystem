using MassTransit;
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
    public class RoomReservedIntegrationEventHandler(RoomDbContext dbContext, ILogger<RoomReservedIntegrationEventHandler> logger)
        : IConsumer<RoomReservedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<RoomReservedIntegrationEvent> context)
        {
            var evt = context.Message;

            var room = await dbContext.Rooms.FindAsync(evt.RoomId);
            if (room == null)
            {
                logger.LogWarning("Room not found for ID {RoomId}", evt.RoomId);
                return;
            }

            // ❌ Mark the room as unavailable
            room.IsAvailable = false;

            await dbContext.SaveChangesAsync();

            logger.LogInformation("Room {RoomId} marked as unavailable due to reservation {ReservationId}.",
                evt.RoomId, evt.ReservationId);
        }
    }
}
