using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Reporting.Reportings.Features.GetRoomOccupyStats;
using Reporting.Reportings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Reportings.Features.GetReservationReports
{
    public record GetReservationReportsResponse(IEnumerable<ReservationReport> ReservationReports);

    public class GetReservationReportsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/report/reservationreport", async (ISender sender) =>
            {
                var result = await sender.Send(new GetReservationReportsQuery());
                var response = new GetReservationReportsResponse(result.ReservationReports);
                return Results.Ok(response);
            })
                .WithName("GetReservationReports")
                .Produces<GetReservationReportsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("GetReservationReports")
                .WithDescription("GetReservationReports")
                ;
        }
    }
}
