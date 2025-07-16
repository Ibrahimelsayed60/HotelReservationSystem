using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Reservation.Reservations.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Features.CreateReservation
{
    public record CreateReservationRequest(ReservationDto reservation);

    public record CreateReservationResponse(Guid Id);

    public class CreateReservationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/reservations", async ( CreateReservationRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateReservationCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateReservationResponse>();

                return Results.Created($"/Reservations/{response.Id}", response);
            })
                .WithName("CreateReservation")
                .Produces<CreateReservationResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Reservation")
                .WithDescription("Create Reservation")
                ;
        }
    }
}
