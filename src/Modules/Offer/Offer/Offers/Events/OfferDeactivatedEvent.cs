using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Events
{
    public record OfferDeactivatedEvent(Guid OfferId, DateTime DeactivatedAt) :IDomainEvent;
    
}
