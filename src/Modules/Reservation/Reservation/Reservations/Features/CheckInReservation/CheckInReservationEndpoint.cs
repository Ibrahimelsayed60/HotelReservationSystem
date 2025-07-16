using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Reservation.Reservations.Features.ConfirmPaymentReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Features.CheckInReservation
{
    public record CheckInReservationRequest(Guid ReservationId);

    public record CheckInReservationResponse(bool IsSuccess);

    public class CheckInReservationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/reservations/{id}/checkin", async (CheckInReservationRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckInReservationCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CheckInReservationResponse>();

                return Results.Ok(response);
            })
                .WithName("CheckInReservation")
                .Produces<CheckInReservationResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("CheckIn Reservation")
                .WithDescription("CheckIn Reservation");
        }
    }
}
