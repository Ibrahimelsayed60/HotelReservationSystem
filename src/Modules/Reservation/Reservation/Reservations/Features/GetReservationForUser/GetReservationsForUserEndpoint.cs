using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Reservation.Reservations.Dtos;
using Reservation.Reservations.Features.GetAllReservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Features.GetReservationForUser
{

    public record GetReservationsForUserResponse(IEnumerable<ReservationDto> reservations);

    public class GetReservationsForUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/reservations/user/{userId}", async (Guid userId, ISender sender) =>
            {
                var reservations = await sender.Send(new GetReservationForUserQuery(userId));

                var response = new GetReservationsForUserResponse(reservations.reservations);

                return Results.Ok(response);

            })
                .WithName("GetAllReservationsForUser")
                .Produces<GetReservationsForUserResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get All Reservations For User")
                .WithDescription("Get All Reservations")
                ;
        }
    }
}
