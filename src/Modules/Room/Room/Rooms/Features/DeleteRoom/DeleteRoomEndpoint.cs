using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.Features.DeleteRoom
{

    public record DeleteRoomResponse(bool IsSuccess);

    public class DeleteRoomEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Rooms/{Id}", async (Guid Id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteRoomCommand(Id));

                var response = result.Adapt<DeleteRoomResponse>();

                return Results.Ok(response);
            })
                .WithName("DeleteRoom")
                .Produces<DeleteRoomResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("DeleteRoom")
                .WithDescription("Delete Room");
        }
    }
}
