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

namespace Offer.Offers.Features.GetAllOffers
{

    public record GetAllOffersQuery():IQuery<GetAllOffersResult>;

    public record GetAllOffersResult(IEnumerable<OfferDto> Offers);

    public class GetAllOffersHandler(OfferDbContext dbContext) : IQueryHandler<GetAllOffersQuery, GetAllOffersResult>
    {
        public async Task<GetAllOffersResult> Handle(GetAllOffersQuery request, CancellationToken cancellationToken)
        {
            var offers = await dbContext.Offers.AsNoTracking().ToListAsync(cancellationToken);

            var offersDto = offers.Adapt<List<OfferDto>>();

            return new GetAllOffersResult(offersDto);
        }
    }
}
