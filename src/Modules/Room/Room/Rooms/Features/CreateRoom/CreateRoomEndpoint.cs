using Carter;
using Mapster;
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

namespace Room.Rooms.Features.CreateRoom
{

    public record CreateRoomRequest(RoomDto Room);

    public record CreateRoomResponse(Guid Id);

    public class CreateRoomEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Rooms", async (CreateRoomRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateRoomCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateRoomResponse>();

                return Results.Created($"/Rooms/{response.Id}", response);
            })
                .WithName("CreateRoom")
                .Produces<CreateRoomResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Room")
                .WithDescription("Create Room");
        }
    }
}
