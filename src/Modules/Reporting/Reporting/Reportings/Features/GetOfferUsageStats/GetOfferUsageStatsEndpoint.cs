using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Reporting.Reportings.Features.GetReservationReports;
using Reporting.Reportings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Features.GetOfferUsageStats
{
    public record GetOfferUsageStatsResponse(IEnumerable<OfferUsageReport> UsageReports);

    public class GetOfferUsageStatsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/report/offerusage", async (ISender sender) =>
            {
                var result = await sender.Send(new GetOfferUsageStatsQuery());
                var response = new GetOfferUsageStatsResponse(result.UsageReports);
                return Results.Ok(response);
            })
                .WithName("GetOfferUsageStats")
                .Produces<GetOfferUsageStatsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("GetOfferUsageStats")
                .WithDescription("GetOfferUsageStats")
                ;
        }
    }
}
