using MassTransit;
using Reservation.Reservations.Services;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.EventHandlers
{
    public class OfferDeactivatedIntegrationEventHandler(IOfferCacheService offerCache) : IConsumer<OfferDeactivatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<OfferDeactivatedIntegrationEvent> context)
        {
            var offerId = context.Message.OfferId;
            await offerCache.RemoveOfferAsync(offerId);
        }
    }
}
