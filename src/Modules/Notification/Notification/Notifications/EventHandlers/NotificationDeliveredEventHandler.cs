using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Notifications.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.EventHandlers
{
    public class NotificationDeliveredEventHandler(ILogger<NotificationDeliveredEventHandler> logger) : INotificationHandler<NotificationDeliveredEvent>
    {
        public Task Handle(NotificationDeliveredEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
