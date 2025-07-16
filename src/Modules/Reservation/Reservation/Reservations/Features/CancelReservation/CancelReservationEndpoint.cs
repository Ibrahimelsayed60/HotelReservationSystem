using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Features.CancelReservation
{
    public record CancelReservationRequest(Guid ReservationId);

    public record CancelReservationResponse(bool IsSuccess);

    public class CancelReservationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/reservations/{id}/cancel", async (CancelReservationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new CancelReservarionCommand(request.ReservationId));

                var response = result.Adapt<CancelReservationRequest>();

                return Results.Ok(response);
            })
                .WithName("CancelReservation")
                .Produces<CancelReservationResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Cancel Reservation")
                .WithDescription("Cancel Reservation");
        }
    }
}
