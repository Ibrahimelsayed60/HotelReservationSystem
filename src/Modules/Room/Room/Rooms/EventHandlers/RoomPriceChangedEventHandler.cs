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
    public class RoomPriceChangedEventHandler(ILogger<RoomPriceChangedEventHandler> logger) : INotificationHandler<RoomPriceChangedEvent>
    {
        public Task Handle(RoomPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain EVent handled {DomainEVent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
