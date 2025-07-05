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

namespace Room.Rooms.Features.GetAllFacilities
{
    public record GetAllFacilitiesResponse(IEnumerable<FacilityDto> Facilities);

    public class GetAllFacilitiesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Facilities", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllFacilitiesQuery());

                var response = result.Adapt<GetAllFacilitiesResponse>();

                return Results.Ok(response);
            })
                .WithName("GetAllFacilities")
                .Produces<GetAllFacilitiesResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get all Facilities")
                .WithDescription("Get all Facilities")
                ;
        }
    }
}
