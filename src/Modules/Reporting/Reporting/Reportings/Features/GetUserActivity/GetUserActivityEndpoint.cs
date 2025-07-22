using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Reporting.Reportings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Features.GetUserActivity
{

    public record GetUserActivityResponse(IEnumerable<UserActivityReport> UserActivities);

    public class GetUserActivityEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/reports/useractivity", async (ISender sender) =>
            {
                var result = await sender.Send(new GetUserActivityQuery());

                var response = result.Adapt<GetUserActivityResponse>();

                return Results.Ok(response);

            })
                .WithName("GetUserActivity")
                .Produces<GetUserActivityResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get UserActivity")
                .WithDescription("Get UserActivity")
                ;
        }
    }
}
