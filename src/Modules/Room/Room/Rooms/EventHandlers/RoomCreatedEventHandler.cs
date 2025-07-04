using MediatR;
using Microsoft.Extensions.Logging;
using Room.Rooms.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.EventHandlers
{
    public class RoomCreatedEventHandler(ILogger<RoomCreatedEventHandler> logger) : INotificationHandler<RoomCreatedEvent>
    {
        public Task Handle(RoomCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
