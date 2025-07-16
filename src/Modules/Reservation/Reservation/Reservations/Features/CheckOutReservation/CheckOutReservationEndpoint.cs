using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Reservation.Reservations.Features.CheckInReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Features.CheckOutReservation
{
    public record CheckOutReservationRequest(Guid ReservationId);

    public record CheckOutReservationResponse(bool IsSuccess);

    public class CheckOutReservationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/reservations/{id}/checkout", async(CheckOutReservationRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckOutReservationCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CheckOutReservationResponse>();

                return Results.Ok(response);
            })
                .WithName("CheckOutReservation")
                .Produces<CheckOutReservationResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("CheckOut Reservation")
                .WithDescription("CheckOut Reservation");
        }
    }
}
