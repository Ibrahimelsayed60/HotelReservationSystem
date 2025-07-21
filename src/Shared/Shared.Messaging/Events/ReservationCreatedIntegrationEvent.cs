using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging.Events
{
    public record ReservationCreatedIntegrationEvent:IntegrationEvent
    {

        public Guid ReservationId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
