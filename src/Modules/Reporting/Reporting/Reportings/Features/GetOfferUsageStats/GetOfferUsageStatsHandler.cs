using Microsoft.EntityFrameworkCore;
using Reporting.Data;
using Reporting.Reportings.Models;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Features.GetOfferUsageStats
{
    public record GetOfferUsageStatsQuery():IQuery<GetOfferUsageStatsResult>;

    public record GetOfferUsageStatsResult(IEnumerable<OfferUsageReport> UsageReports);

    internal class GetOfferUsageStatsHandler(ReportingDbContext dbContext) : IQueryHandler<GetOfferUsageStatsQuery, GetOfferUsageStatsResult>
    {
        public async Task<GetOfferUsageStatsResult> Handle(GetOfferUsageStatsQuery request, CancellationToken cancellationToken)
        {
            var offers = await dbContext.OfferUsageReports.ToListAsync(cancellationToken);

            return new GetOfferUsageStatsResult(offers);
        }
    }
}
