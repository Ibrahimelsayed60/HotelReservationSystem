using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Models
{
    public class NotificationTemplate:Entity<Guid>
    {
        public string EventType { get; set; }         // e.g., "ReservationPaid", "FeedbackReplied"
        public string TitleTemplate { get; set; }     // e.g., "Your reservation has been confirmed"
        public string MessageTemplate { get; set; }   // e.g., "Thank you for your reservation on {{date}}."

    }
}
