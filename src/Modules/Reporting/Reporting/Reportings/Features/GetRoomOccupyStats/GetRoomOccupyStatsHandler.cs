using Microsoft.EntityFrameworkCore;
using Reporting.Data;
using Reporting.Reportings.Models;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Features.GetRoomOccupyStats
{
    public record GetRoomOccupyStatsQuery():IQuery<GetRoomOccupyStatsResult>;

    public record GetRoomOccupyStatsResult(IEnumerable<RoomOccupancyReport> OccupancyReports);

    public class GetRoomOccupyStatsHandler(ReportingDbContext dbContext) : IQueryHandler<GetRoomOccupyStatsQuery, GetRoomOccupyStatsResult>
    {
        public async Task<GetRoomOccupyStatsResult> Handle(GetRoomOccupyStatsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await dbContext.RoomOccupancyReports.ToListAsync(cancellationToken);

            return new GetRoomOccupyStatsResult(rooms);

        }
    }
}
