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

namespace Offer.Offers.Features.GetOfferById
{

    public record GetOfferByIdQuery(Guid Id):IQuery<GetOfferByIdResult>;

    public record GetOfferByIdResult(OfferDto Offer);

    public class GetOfferByIdHandler(OfferDbContext dbContext) : IQueryHandler<GetOfferByIdQuery, GetOfferByIdResult>
    {
        public async Task<GetOfferByIdResult> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
        {
            var offer = await dbContext.Offers.Where(o => o.Id == request.Id).FirstOrDefaultAsync();

            if (offer == null)
            {
                throw new Exception($"Offer with Id: {request.Id} not found");
            }

            var offerDto = offer.Adapt<OfferDto>();

            return new GetOfferByIdResult(offerDto);

        }
    }
}
