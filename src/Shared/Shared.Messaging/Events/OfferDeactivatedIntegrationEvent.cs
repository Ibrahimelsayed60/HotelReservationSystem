using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging.Events
{
    public class OfferDeactivatedIntegrationEvent
    {
        public Guid OfferId { get; set; }
        public DateTime DeactivatedAt { get; set; }

    }
}
