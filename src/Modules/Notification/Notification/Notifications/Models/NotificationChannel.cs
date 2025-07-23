using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Models
{
    public class NotificationChannel:Entity<Guid>
    {
        public Guid NotificationId { get; set; }
        public Notification Notification { get; set; }

        public string ChannelType { get; set; } // e.g., "InApp", "Email", "SMS"
        public bool IsSent { get; set; }
        public DateTime? SentAt { get; set; }

    }
}
