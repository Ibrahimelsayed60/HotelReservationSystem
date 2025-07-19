using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Services
{
    public interface IOfferCacheService
    {
        Task CacheOfferAsync(CachedOffer offer);
        Task<List<CachedOffer>> GetAllOffersAsync();
        Task RemoveOfferAsync(Guid offerId);

    }
}
