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

namespace Reservation.Reservations.Features.GetReservationById
{

    public record GetReservationByIdResponse(ReservationDto reservation);

    public class GetReservationByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/reservation/{id}", async (Guid id, ISender sender) =>
            {

                var result = await sender.Send(new GetReservationByIdQuery(id));

                var response = new GetReservationByIdResponse(result.reservation);

                return Results.Ok(response);

            })
                .WithName("GetReservationById")
                .Produces<GetReservationByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Reservation By Id")
                .WithDescription("Get Reservation By Id")

                ;
        }
    }
}
