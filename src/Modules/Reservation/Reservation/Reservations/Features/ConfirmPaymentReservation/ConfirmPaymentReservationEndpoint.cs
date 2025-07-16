using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Reservation.Reservations.Features.CancelReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Features.ConfirmPaymentReservation
{
    public record ConfirmReservationPaymentRequest(Guid ReservationId, decimal Amount, string PaymentMethod);

    public record ConfirmReservationPaymentResponse(bool IsSuccess);

    public class ConfirmPaymentReservationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/reservations/{id}/confirmpayment", async (ConfirmReservationPaymentRequest request, ISender sender) =>
            {
                var command = request.Adapt<ConfirmReservationPaymentCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<ConfirmReservationPaymentResponse>();

                return Results.Ok(response);
            })
                .WithName("ConfirmReservation")
                .Produces<ConfirmReservationPaymentResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Confirm Reservation")
                .WithDescription("Confirm Reservation");
        }
    }
}
