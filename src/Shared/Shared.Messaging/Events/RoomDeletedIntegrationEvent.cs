using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging.Events
{
    public record RoomDeletedIntegrationEvent:IntegrationEvent
    {

        public Guid RoomId { get; set; }
        public DateTime DeletedAt { get; set; } = DateTime.UtcNow;

    }
}
