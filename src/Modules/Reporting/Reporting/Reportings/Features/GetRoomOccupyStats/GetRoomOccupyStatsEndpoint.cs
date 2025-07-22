using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Reporting.Reportings.Features.GetUserActivity;
using Reporting.Reportings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Features.GetRoomOccupyStats
{

    public record GetRoomOccupyStatsResponse(IEnumerable<RoomOccupancyReport> OccupancyReports);

    public class GetRoomOccupyStatsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/report/roomoccupy", async (ISender sender) =>
            {
                var rooms = await sender.Send(new GetRoomOccupyStatsQuery());

                var response = new GetRoomOccupyStatsResponse(rooms.OccupancyReports);

                return Results.Ok(response);
            })
                .WithName("GetRoomOccupyStats")
                .Produces<GetRoomOccupyStatsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get RoomOccupyStats")
                .WithDescription("Get RoomOccupyStats")
                ;
        }
    }
}
