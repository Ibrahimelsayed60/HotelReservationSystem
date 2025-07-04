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

namespace Room.Rooms.Features.UpdateRoom
{

    public record UpdateRoomRequest(RoomDto Room);

    public record UpdateRoomResponse(bool IsSuccess);

    public class UpdateRoomEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/Rooms", async (UpdateRoomRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateRoomCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateRoomResponse>();

                return Results.Ok(response);

            })
                .WithName("UpdateRoom")
                .Produces<UpdateRoomResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Room")
                .WithDescription("Update Room");
        }
    }
}
