using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reservation.Data;
using Reservation.Reservations.Features.CancelReservation;
using Reservation.Reservations.Features.UpdateRoomInReservation;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.EventHandlers
{
    public class RoomDeletedIntegrationEventHandler(ReservationDbContext dbContext,ISender sender, ILogger<RoomDeletedIntegrationEventHandler> logger)
        : IConsumer<RoomDeletedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<RoomDeletedIntegrationEvent> context)
        {
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

            var reservation = (await dbContext.Reservations.Where(r => r.RoomId == context.Message.RoomId).FirstOrDefaultAsync());

            var command = new CancelReservarionCommand(reservation.Id);

            var result = await sender.Send(command);

            if (!result.IsSuccess)
            {
                logger.LogError("Error Canceling Reservation: {ReservationId}", reservation.Id);
            }

            logger.LogInformation("Delete Room with id: {ProductId} updated in Reservation", reservation.RoomId);
        }
    }
}
