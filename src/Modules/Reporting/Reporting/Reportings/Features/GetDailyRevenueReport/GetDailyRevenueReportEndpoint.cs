using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Reporting.Reportings.Features.GetOfferUsageStats;
using Reporting.Reportings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Features.GetDailyRevenueReport
{
    public record GetDailyRevenueReportResponse(IEnumerable<DailyRevenueReport> RevenueReports);

    public class GetDailyRevenueReportEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/report/dailyreport", async (ISender sender) =>
            {
                var result = await sender.Send(new GetDailyRevenueReportQuery());
                var response = new GetDailyRevenueReportResponse(result.RevenueReports);
                return Results.Ok(response);
            })
                .WithName("GetDailyRevenueReport")
                .Produces<GetDailyRevenueReportResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("GetDailyRevenueReport")
                .WithDescription("GetDailyRevenueReport")
                ;
        }
    }
}
