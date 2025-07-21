using Microsoft.Extensions.Caching.Distributed;
using Reservation.Reservations.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reservation.Reservations.Services
{
    public class RedisOfferCacheService:IOfferCacheService
    {

        private readonly IDistributedCache _cache;
        private const string Key = "offers:list";

        public RedisOfferCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task CacheOfferAsync(CachedOffer offer)
        {
            var offers = await GetAllOffersAsync();
            offers.RemoveAll(o => o.OfferId == offer.OfferId);
            offers.Add(offer);

            var serialized = JsonSerializer.Serialize(offers);
            await _cache.SetStringAsync(Key, serialized);
        }

        public async Task<List<CachedOffer>> GetAllOffersAsync()
        {
            var data = await _cache.GetStringAsync(Key);
            return string.IsNullOrWhiteSpace(data)
                ? new List<CachedOffer>()
                : JsonSerializer.Deserialize<List<CachedOffer>>(data);
        }

        public async Task RemoveOfferAsync(Guid offerId)
        {
            var offers = await GetAllOffersAsync();
            offers.RemoveAll(o => o.OfferId == offerId);
            await _cache.SetStringAsync(Key, JsonSerializer.Serialize(offers));
        }

    }
}
