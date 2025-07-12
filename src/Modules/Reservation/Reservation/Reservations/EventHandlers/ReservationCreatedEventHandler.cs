using MediatR;
using Microsoft.Extensions.Logging;
using Reservation.Reservations.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.EventHandlers
{
    public class ReservationCreatedEventHandler(ILogger<ReservationCreatedEventHandler> logger) : INotificationHandler<ReservationCreatedEvent>
    {
        public Task Handle(ReservationCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
