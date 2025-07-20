using MassTransit;
using Microsoft.Extensions.Logging;
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
    public class OfferCreatedIntegrationEventHandler(IOfferCacheService offerCache, ILogger<OfferCreatedIntegrationEventHandler> logger)
        : IConsumer<OfferCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<OfferCreatedIntegrationEvent> context)
        {
            var offer = context.Message;

            var cachedOffer = new CachedOffer
            {
                OfferId = offer.OfferId,
                Title = offer.Title,
                DiscountPercentage = offer.DiscountPercentage,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                RoomIds = new List<Guid>() // if needed
            };

            await offerCache.CacheOfferAsync(cachedOffer);
        }
    }
}
