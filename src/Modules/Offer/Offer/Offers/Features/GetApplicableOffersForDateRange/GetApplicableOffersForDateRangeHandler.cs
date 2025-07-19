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

namespace Offer.Offers.Features.GetApplicableOffersForDateRange
{
    public record GetApplicableOffersForDateRangeQuery(DateTime CheckInDate, DateTime CheckOutDate) :IQuery<GetApplicableOffersForDateRangeResult>;

    public record GetApplicableOffersForDateRangeResult(IEnumerable<OfferDto> Offers);

    public class GetApplicableOffersForDateRangeHandler(OfferDbContext dbContext) : IQueryHandler<GetApplicableOffersForDateRangeQuery, GetApplicableOffersForDateRangeResult>
    {
        public async Task<GetApplicableOffersForDateRangeResult> Handle(GetApplicableOffersForDateRangeQuery request, CancellationToken cancellationToken)
        {
            var offers = await dbContext.Offers.Where(o => request.CheckInDate >= o.StartDate && request.CheckOutDate <= o.EndDate).ToListAsync();

            var offersDto = offers.Adapt<List<OfferDto>>();

            return new GetApplicableOffersForDateRangeResult(offersDto);

        }
    }
}
