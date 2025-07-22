using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Reporting.Data;
using Reporting.Reportings.Models;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Features.ExportReportsToCSV
{
    public record ExportReportsToCSVQuery(DateTime From, DateTime To) :IQuery<ExportReportsToCSVResult>;

    public record ExportReportsToCSVResult(Stream Stream);

    public class ExportReportsToCSVHandler(ReportingDbContext dbContext) : IQueryHandler<ExportReportsToCSVQuery, ExportReportsToCSVResult>
    {
        public async Task<ExportReportsToCSVResult> Handle(ExportReportsToCSVQuery request, CancellationToken cancellationToken)
        {
            var reservations = await dbContext.ReservationReports
            .Where(r => r.CreatedAt >= request.From && r.CreatedAt <= request.To)
            .ToListAsync(cancellationToken);

            var stream = new MemoryStream();
            using var writer = new StreamWriter(stream, leaveOpen: true);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteHeader<ReservationReport>();
            await csv.NextRecordAsync();
            await csv.WriteRecordsAsync(reservations, cancellationToken);

            await writer.FlushAsync();
            stream.Position = 0;

            return new ExportReportsToCSVResult(stream);
        }
    }
}
