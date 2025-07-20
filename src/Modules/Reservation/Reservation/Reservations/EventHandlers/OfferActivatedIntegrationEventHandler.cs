using MassTransit;
using Reservation.Reservations.Dtos;
using Reservation.Reservations.Services;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.EventHandlers
{
    public class OfferActivatedIntegrationEventHandler(IOfferCacheService _cache) : IConsumer<OfferActivatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<OfferActivatedIntegrationEvent> context)
        {
            var msg = context.Message;

            var cached = new CachedOffer
            {
                OfferId = msg.OfferId,
                Title = msg.Title,
                DiscountPercentage = msg.DiscountPercentage,
                StartDate = msg.StartDate,
                EndDate = msg.EndDate,
                RoomIds = msg.RoomIds
            };

            await _cache.CacheOfferAsync(cached);
        }
    }
}
