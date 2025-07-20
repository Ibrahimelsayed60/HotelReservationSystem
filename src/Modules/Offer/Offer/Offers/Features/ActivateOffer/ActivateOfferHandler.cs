using MassTransit;
using Microsoft.EntityFrameworkCore;
using Offer.Data;
using Shared.Contracts.CQRS;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.ActivateOffer
{
    public record ActivateOfferCommand(Guid offerId):ICommand<ActivateOfferResult>;

    public record ActivateOfferResult(bool IsSuccess);

    public class ActivateOfferHandler(OfferDbContext dbContext, IBus bus) : ICommandHandler<ActivateOfferCommand, ActivateOfferResult>
    {
        public async Task<ActivateOfferResult> Handle(ActivateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await dbContext.Offers.Where(o => o.Id == request.offerId).FirstOrDefaultAsync();

            if (offer == null)
            {
                throw new Exception($"offer with Id: {request.offerId} not found");
            }

            offer.IsActive = true;

            dbContext.Offers.Update(offer);

            await dbContext.SaveChangesAsync();

            var integrationEvent = new OfferActivatedIntegrationEvent 
            {
                OfferId=offer.Id,
                Title=offer.Title,
                DiscountPercentage=offer.DiscountPercentage,
                StartDate=offer.StartDate,
                EndDate=offer.EndDate,
                RoomIds=offer.OfferRooms.Select(r => r.RoomId).ToList()
            };

            await bus.Publish(integrationEvent);

            return new ActivateOfferResult(true);
        }
    }
}
