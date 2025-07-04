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
    public class RoomFacilityAssignedEventHandler(ILogger<RoomFacilityAssignedEventHandler> logger) : INotificationHandler<RoomFacilityAssignedEvent>
    {
        public Task Handle(RoomFacilityAssignedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain EVent handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
