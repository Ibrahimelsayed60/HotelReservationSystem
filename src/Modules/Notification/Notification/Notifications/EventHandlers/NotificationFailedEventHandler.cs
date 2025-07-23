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
    public class NotificationFailedEventHandler(ILogger<NotificationFailedEventHandler> logger) : INotificationHandler<NotificationFailedEvent>
    {
        public Task Handle(NotificationFailedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
