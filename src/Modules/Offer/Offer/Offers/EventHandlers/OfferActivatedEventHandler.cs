using MediatR;
using Microsoft.Extensions.Logging;
using Offer.Offers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.EventHandlers
{
    public class OfferActivatedEventHandler(ILogger<OfferActivatedEventHandler> logger) : INotificationHandler<OfferActivatedEvent>
    {
        public Task Handle(OfferActivatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
