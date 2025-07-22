using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Reporting.Reportings.Features.GetDailyRevenueReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Features.ExportReportsToCSV
{
    public record ExportReportsToCSVResponse(Stream Stream);

    public class ExportReportsToCSVEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/report/exportreport", async ([FromQuery] DateTime from, [FromQuery] DateTime to, ISender sender) =>
            {
                var stream = await sender.Send(new ExportReportsToCSVQuery(from, to));
                return Results.File(stream.Stream, "text/csv", "reservations.csv");
            })
                .WithName("ExportReportsToCSV")
                .Produces<ExportReportsToCSVResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("ExportReportsToCSV")
                .WithDescription("ExportReportsToCSV")
                ;
        }
    }
}
