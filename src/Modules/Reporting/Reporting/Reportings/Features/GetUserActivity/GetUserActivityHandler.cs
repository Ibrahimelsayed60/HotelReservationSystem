using Microsoft.EntityFrameworkCore;
using Reporting.Data;
using Reporting.Reportings.Models;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Features.GetUserActivity
{
    public record GetUserActivityQuery():IQuery<GetUserActivityResult>;

    public record GetUserActivityResult(IEnumerable<UserActivityReport> UserActivityReports);

    public class GetUserActivityHandler(ReportingDbContext dbContext) : IQueryHandler<GetUserActivityQuery, GetUserActivityResult>
    {
        public async Task<GetUserActivityResult> Handle(GetUserActivityQuery request, CancellationToken cancellationToken)
        {
            var userActivities = await dbContext.UserActivityReports.ToListAsync();

            return new GetUserActivityResult(userActivities);
        }
    }
}
