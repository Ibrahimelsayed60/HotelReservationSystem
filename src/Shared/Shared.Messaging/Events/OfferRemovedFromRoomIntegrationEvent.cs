using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging.Events
{
    public record OfferRemovedFromRoomIntegrationEvent:IntegrationEvent
    {
        public Guid OfferId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime RemovedAt { get; set; }

    }
}
