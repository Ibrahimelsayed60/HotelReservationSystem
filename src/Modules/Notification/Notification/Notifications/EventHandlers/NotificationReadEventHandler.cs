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
    public class NotificationReadEventHandler(ILogger<NotificationReadEventHandler> logger) : INotificationHandler<NotificationReadEvent>
    {
        public Task Handle(NotificationReadEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
