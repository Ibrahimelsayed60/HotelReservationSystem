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
    public class RoomDeletedEventHandler(ILogger<RoomCreatedEventHandler> logger) : INotificationHandler<RoomDeletedEvent>
    {
        public Task Handle(RoomDeletedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
