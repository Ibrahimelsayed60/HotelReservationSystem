using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Events
{
    public record NotificationFailedEvent(Guid NotificationId, string Reason) :IDomainEvent;
    
}
