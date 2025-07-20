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
    public class OfferRemovedFromRoomHandler(IOfferCacheService offerCache) : IConsumer<OfferRemovedFromRoomIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<OfferRemovedFromRoomIntegrationEvent> context)
        {
            var offerId = context.Message.OfferId;
            var roomId = context.Message.RoomId;

            var offers = await offerCache.GetAllOffersAsync();

            var offer = offers.FirstOrDefault(o => o.OfferId == offerId);
            if (offer is null) return;

            offer.RoomIds.Remove(roomId);
            await offerCache.CacheOfferAsync(offer); // overwrite with updated RoomIds
        }
    }
}
