using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging.Events
{
    public record RoomUpdatedEventIntegrationEvent : IntegrationEvent
    {

        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
