using Microsoft.EntityFrameworkCore;
using Offer.Data;
using Offer.Offers.Dtos;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.UpdateOffer
{
    public record UpdateOfferCommand(OfferDto Offer):ICommand<UpdateOfferResult>;

    public record UpdateOfferResult(bool IsSuccess);

    public class UpdateOfferHandler(OfferDbContext dbContext) : ICommandHandler<UpdateOfferCommand, UpdateOfferResult>
    {
        public async Task<UpdateOfferResult> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await dbContext.Offers.Where(o => o.Id == request.Offer.Id).FirstOrDefaultAsync();

            if (offer == null)
            {
                throw new Exception($"Offer with Id: {request.Offer.Id} not found");
            }

            offer.UpdateDetails(request.Offer.Title, request.Offer.Description, request.Offer.DiscountPercentage, request.Offer.StartDate, request.Offer.EndDate);

            await dbContext.SaveChangesAsync();

            return new UpdateOfferResult(true);
        }
    }
}
