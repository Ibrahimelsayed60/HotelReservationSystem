using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Reservation.Reservations.Dtos;
using Reservation.Reservations.Features.GetReservationForUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Features.GetReservationsForRoom
{

    public record GetReservationsForRoomResponse(IEnumerable<ReservationDto> reservations);

    public class GetReservationsForRoomEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/reservarion/room/{roomId}", async (Guid roomId, ISender sender) =>
            {
                var result = await sender.Send(new GetReservationsForRoomQuery(roomId));

                var response = new GetReservationsForRoomResponse(result.reservations);

                return Results.Ok(response);
            })
                .WithName("GetAllReservationsForRoom")
                .Produces<GetReservationsForRoomResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get All Reservations For Room")
                .WithDescription("Get All Reservations For Room")
                ;
        }
    }
}
