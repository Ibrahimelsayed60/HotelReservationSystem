using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Features.ExportNotificationsToCsv
{
    public record ExportNotificationsToCsvResponse(Stream Stream);

    internal class ExportNotificationsToCsvEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/notification/csv", async ([FromQuery] DateTime from, [FromQuery] DateTime to, ISender sender) =>
            {
                var stream = await sender.Send(new ExportNotificationsToCsvQuery(from, to));
                return Results.File(stream.Stream, "text/csv", "reservations.csv");
            })
                .WithName("ExportNotificationsToCsv")
                .Produces<ExportNotificationsToCsvResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("ExportNotificationsToCsv")
                .WithDescription("ExportNotificationsToCsv")
                ;
        }
    }
}
