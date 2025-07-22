using Microsoft.EntityFrameworkCore;
using Reporting.Data;
using Reporting.Reportings.Models;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Features.GetDailyRevenueReport
{
    public record GetDailyRevenueReportQuery():IQuery<GetDailyRevenueReportResult>;

    public record GetDailyRevenueReportResult(IEnumerable<DailyRevenueReport> RevenueReports);

    public class GetDailyRevenueReportHandler(ReportingDbContext dbContext) : IQueryHandler<GetDailyRevenueReportQuery, GetDailyRevenueReportResult>
    {
        public async Task<GetDailyRevenueReportResult> Handle(GetDailyRevenueReportQuery request, CancellationToken cancellationToken)
        {
            var dailyReports = await dbContext.DailyRevenueReports.ToListAsync(cancellationToken);

            return new GetDailyRevenueReportResult(dailyReports);
        }
    }
}
