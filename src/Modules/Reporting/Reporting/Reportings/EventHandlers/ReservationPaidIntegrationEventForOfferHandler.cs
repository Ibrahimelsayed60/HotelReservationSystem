using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reporting.Data;
using Reporting.Reportings.Models;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.EventHandlers
{
    public class ReservationPaidIntegrationEventForOfferHandler(ReportingDbContext dbContext, ILogger<ReservationPaidIntegrationEventForOfferHandler> logger)
        : IConsumer<ReservationPaidIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ReservationPaidIntegrationEvent> context)
        {
            var msg = context.Message;

            if (msg.AppliedOfferId is null || msg.DiscountPercentage == null)
                return; // no offer used, skip

            var offer = await dbContext.OfferUsageReports
                .FirstOrDefaultAsync(x => x.OfferId == msg.AppliedOfferId.Value);

            var discountAmount = msg.TotalAmount * (msg.DiscountPercentage.Value / 100);

            if (offer == null)
            {
                offer = new OfferUsageReport
                {
                    OfferId = msg.AppliedOfferId.Value,
                    Title = msg.OfferTitle,
                    TimesUsed = 1,
                    TotalDiscountGiven = discountAmount
                };
                dbContext.OfferUsageReports.Add(offer);
            }
            else
            {
                offer.TimesUsed += 1;
                offer.TotalDiscountGiven += discountAmount;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
