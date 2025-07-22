using CsvHelper;
using Feedback.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Features.ExportFeedbackToCSV
{

    public record ExportFeedbackToCsvQuery(DateTime From, DateTime To):IQuery<ExportFeedbackToCsvResult>;

    public record ExportFeedbackToCsvResult(Stream Stream);

    internal class ExportFeedbackToCSVHandler(FeedbackDbContext dbContext) : IQueryHandler<ExportFeedbackToCsvQuery, ExportFeedbackToCsvResult>
    {
        public async Task<ExportFeedbackToCsvResult> Handle(ExportFeedbackToCsvQuery request, CancellationToken cancellationToken)
        {
            var feedbacks = await dbContext.Feedbacks
            .Where(f => f.SubmittedAt >= request.From && f.SubmittedAt <= request.To)
            .OrderByDescending(f => f.SubmittedAt)
            .ToListAsync(cancellationToken);

            var stream = new MemoryStream();
            using var writer = new StreamWriter(stream, leaveOpen: true);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteField("Id");
            csv.WriteField("ReservationId");
            csv.WriteField("UserId");
            csv.WriteField("Rating");
            csv.WriteField("Comment");
            csv.WriteField("SubmittedAt");
            csv.WriteField("IsResolved");
            await csv.NextRecordAsync();

            foreach (var f in feedbacks)
            {
                csv.WriteField(f.Id);
                csv.WriteField(f.ReservationId);
                csv.WriteField(f.UserId);
                csv.WriteField(f.Rating);
                csv.WriteField(f.Comment);
                csv.WriteField(f.SubmittedAt);
                csv.WriteField(f.IsResolved);
                await csv.NextRecordAsync();
            }

            await writer.FlushAsync();
            stream.Position = 0;
            return new ExportFeedbackToCsvResult( stream);
        }
    }
}
