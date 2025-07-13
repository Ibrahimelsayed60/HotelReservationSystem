using Carter;
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

namespace Reservation.Reservations.Features.GetAllReservations
{
    public record GetAllReservationsResponse(IEnumerable<ReservationDto> reservations);

    public class GetAllReservationsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/reservations", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllReservationsQuery());

                var response = new GetAllReservationsResponse(result.reservations);

                return Results.Ok(response);
            })
                .WithName("GetAllReservations")
                .Produces<GetAllReservationsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get All Reservations")
                .WithDescription("Get All Reservations")
                ;
        }
    }
}
