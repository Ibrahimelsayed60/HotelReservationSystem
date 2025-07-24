using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Features.ExportNotificationsToCsv
{
    public record ExportNotificationsToCsvQuery(DateTime From, DateTime To):IQuery<ExportNotificationsToCsvResult>;

    public record ExportNotificationsToCsvResult(Stream Stream);

    internal class ExportNotificationsToCsvHandler(NotificationDbContext dbContext) : IQueryHandler<ExportNotificationsToCsvQuery, ExportNotificationsToCsvResult>
    {
        public async Task<ExportNotificationsToCsvResult> Handle(ExportNotificationsToCsvQuery request, CancellationToken cancellationToken)
        {
            var notifications = await dbContext.Notifications
            .Where(n => n.CreatedAt >= request.From && n.CreatedAt <= request.To)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync(cancellationToken);

            var stream = new MemoryStream();
            using var writer = new StreamWriter(stream, leaveOpen: true);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteField("Id");
            csv.WriteField("UserId");
            csv.WriteField("Title");
            csv.WriteField("Message");
            csv.WriteField("Type");
            csv.WriteField("IsRead");
            csv.WriteField("CreatedAt");
            await csv.NextRecordAsync();

            foreach (var n in notifications)
            {
                csv.WriteField(n.Id);
                csv.WriteField(n.UserId);
                csv.WriteField(n.Title);
                csv.WriteField(n.Message);
                csv.WriteField(n.Type);
                csv.WriteField(n.IsRead);
                csv.WriteField(n.CreatedAt);
                await csv.NextRecordAsync();
            }

            await writer.FlushAsync();
            stream.Position = 0;
            return new ExportNotificationsToCsvResult(stream);
        }
    }
}
