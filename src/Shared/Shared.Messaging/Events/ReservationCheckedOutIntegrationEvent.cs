using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging.Events
{
    public record ReservationCheckedOutIntegrationEvent:IntegrationEvent
    {
        public Guid ReservationId { get; set; }
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
