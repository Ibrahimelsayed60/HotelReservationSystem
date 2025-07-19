using Microsoft.EntityFrameworkCore;
using Offer.Data;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.DeleteOffer
{
    public record DeleteOfferCommand(Guid offerId):ICommand<DeleteOfferResult>;

    public record DeleteOfferResult(bool IsSuccess);
    public class DeleteOfferHandler(OfferDbContext dbContext) : ICommandHandler<DeleteOfferCommand, DeleteOfferResult>
    {
        public async Task<DeleteOfferResult> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await dbContext.Offers.FindAsync(new object[] { request.offerId }, cancellationToken);
            if (offer == null)
                throw new Exception("Offer not found.");

            dbContext.Offers.Remove(offer);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteOfferResult(true);
        }
    }
}
