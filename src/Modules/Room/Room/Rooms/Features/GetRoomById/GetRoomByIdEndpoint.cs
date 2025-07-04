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

namespace Room.Rooms.Features.GetRoomById
{

    public record GetRoomByIdResponse(RoomDto Room);
    public class GetRoomByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Rooms/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetRoomByIdQuery(id));

                var response = result.Adapt<GetRoomByIdResponse>();

                return Results.Ok(response);

            })
            .WithName("GetRoomById")
            .Produces<GetRoomByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Room By Id")
            .WithDescription("Get Room By Id");
        }
    }
}
