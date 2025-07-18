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
    public class OfferDeactivatedEventHandler(ILogger<OfferDeactivatedEvent> logger) : INotificationHandler<OfferDeactivatedEvent>
    {
        public Task Handle(OfferDeactivatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
