using Microsoft.EntityFrameworkCore;
using Reporting.Data;
using Reporting.Reportings.Models;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Features.GetReservationReports
{
    public record GetReservationReportsQuery():IQuery<GetReservationReportsResult>;

    public record GetReservationReportsResult(IEnumerable<ReservationReport> ReservationReports);

    public class GetReservationReportsHandler(ReportingDbContext dbContext) : IQueryHandler<GetReservationReportsQuery, GetReservationReportsResult>
    {
        public async Task<GetReservationReportsResult> Handle(GetReservationReportsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await dbContext.ReservationReports.ToListAsync(cancellationToken);

            return new GetReservationReportsResult(reservations);
        }
    }
}
