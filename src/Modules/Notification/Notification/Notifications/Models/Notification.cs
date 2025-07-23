using Notification.Notifications.Events;
using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Models
{
    public class Notification:Aggregate<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // e.g., "Info", "Warning", "Success", "Error"
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public Notification(Guid userId, string title, string message, string type)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Title = title;
            Message = message;
            Type = type;
            CreatedAt = DateTime.UtcNow;
            IsRead = false;

            AddDomainEvent(new NotificationCreatedEvent(Id));
        }

        public void MarkAsRead()
        {
            if (!IsRead)
            {
                IsRead = true;
                AddDomainEvent(new NotificationReadEvent(Id));
            }
        }

    }
}
