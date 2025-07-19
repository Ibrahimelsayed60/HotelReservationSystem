using Mapster;
using Microsoft.EntityFrameworkCore;
using Offer.Data;
using Offer.Offers.Dtos;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.GetOffersForRoom
{

    public record GetOffersForRoomQuery(Guid RoomId):IQuery<GetOffersForRoomResult>;

    public record GetOffersForRoomResult(IEnumerable<OfferDto> Offers);

    public class GetOffersForRoomHandler(OfferDbContext dbContext) : IQueryHandler<GetOffersForRoomQuery, GetOffersForRoomResult>
    {
        public async Task<GetOffersForRoomResult> Handle(GetOffersForRoomQuery request, CancellationToken cancellationToken)
        {
            var offers = await dbContext.Offers.Where(o => o.OfferRooms.Any(r => r.RoomId == request.RoomId)).ToListAsync();

            var offersDto = offers.Adapt<List<OfferDto>>();

            return new GetOffersForRoomResult(offersDto);
        }
    }
}
