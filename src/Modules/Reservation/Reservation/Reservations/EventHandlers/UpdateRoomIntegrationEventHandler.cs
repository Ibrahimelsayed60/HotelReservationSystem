using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Reservation.Reservations.Features.UpdateRoomInReservation;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.ValidationResultExtensions;

namespace Reservation.Reservations.EventHandlers
{
    public class UpdateRoomIntegrationEventHandler(ISender sender, ILogger<UpdateRoomIntegrationEventHandler> logger)
        : IConsumer<RoomUpdatedEventIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<RoomUpdatedEventIntegrationEvent> context)
        {
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

            var command = new UpdateRoomInReservationCommand(context.Message);

            var result = await sender.Send(command);

            if (!result.IsSuccess)
            {
                logger.LogError("Error updating price in basket for product id: {ProductId}", context.Message.RoomId);
            }

            logger.LogInformation("Price for product id: {ProductId} updated in basket", context.Message.RoomId);
        }
    }
}
