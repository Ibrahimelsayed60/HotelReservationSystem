using MassTransit;
using Microsoft.EntityFrameworkCore;
using Offer.Data;
using Offer.Offers.Features.ActivateOffer;
using Shared.Contracts.CQRS;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.DeactivateOffer
{

    public record DeactivateOfferCommand(Guid offerId):ICommand<DeactivateOfferResult>;

    public record DeactivateOfferResult(bool IsSuccess);

    public class DeactivateOfferHandler(OfferDbContext dbContext, IBus bus) : ICommandHandler<DeactivateOfferCommand, DeactivateOfferResult>
    {
        public async Task<DeactivateOfferResult> Handle(DeactivateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await dbContext.Offers.Where(o => o.Id == request.offerId).FirstOrDefaultAsync();

            if (offer == null)
            {
                throw new Exception($"offer with Id: {request.offerId} not found");
            }

            offer.IsActive = false;

            dbContext.Offers.Update(offer);

            await dbContext.SaveChangesAsync();

            var integrationEvent = new OfferDeactivatedIntegrationEvent { 
                OfferId=offer.Id,
                DeactivatedAt=DateTime.UtcNow
                };

            await bus.Publish(integrationEvent);


            return new DeactivateOfferResult(true);
        }
    }
}
