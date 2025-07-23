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
    public class NotificationCreatedEventHandler(ILogger<NotificationCreatedEvent> logger) : INotificationHandler<NotificationCreatedEvent>
    {
        public Task Handle(NotificationCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
