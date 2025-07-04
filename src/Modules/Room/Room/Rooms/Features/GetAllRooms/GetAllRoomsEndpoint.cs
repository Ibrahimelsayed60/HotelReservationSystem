using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Room.Rooms.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.Features.GetAllRooms
{

    public record GetAllRoomsResponse(IEnumerable<RoomDto> rooms);

    public class GetAllRoomsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Rooms", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllRoomsQuery());

                var response = new GetAllRoomsResponse(result.rooms);

                return Results.Ok(response);

            })
            .WithName("GetRooms")
            .Produces<GetAllRoomsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Rooms")
            .WithDescription("Get Rooms");
        }
    }
}
