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

namespace Room.Rooms.Features.GetFacilityById
{
    public record GetFacilityByIdResponse(FacilityDto Facility);

    internal class GetFacilityByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Facilities/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetFacilityByIdQuery(id));

                var response = result.Adapt<GetFacilityByIdResponse>();

                return Results.Ok(response);

            })
                .WithName("GetFacilityById")
                .Produces<GetFacilityByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Facility By Id")
                .WithDescription("Get Facility By Id")
                ;
        }
    }
}
