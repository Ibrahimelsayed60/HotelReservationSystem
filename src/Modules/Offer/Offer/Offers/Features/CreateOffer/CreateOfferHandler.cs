using MassTransit;
using Offer.Data;
using Offer.Offers.Dtos;
using Offer.Offers.Models;
using Shared.Contracts.CQRS;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.CreateOffer
{
    public record CreateOfferCommand(OfferDto Offer):ICommand<CreateOfferResult>;

    public record CreateOfferResult(Guid offerId);

    public class CreateOfferHandler(OfferDbContext dbContext, IBus bus) : ICommandHandler<CreateOfferCommand, CreateOfferResult>
    {
        public async Task<CreateOfferResult> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = Offer.Offers.Models.Offer.Create(
                        request.Offer.Title, 
                        request.Offer.Description, 
                        request.Offer.DiscountPercentage, 
                        request.Offer.StartDate, 
                        request.Offer.EndDate);


            dbContext.Offers.Add(offer);

            await dbContext.SaveChangesAsync();

            var integrationEvent = new OfferCreatedIntegrationEvent
            {
                OfferId = offer.Id,
                Title = offer.Title,
                DiscountPercentage = offer.DiscountPercentage,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
            };

            await bus.Publish(integrationEvent);

            return new CreateOfferResult(offer.Id);
        }
    }
}
