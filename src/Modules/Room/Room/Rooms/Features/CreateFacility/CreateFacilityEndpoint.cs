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

namespace Room.Rooms.Features.CreateFacility
{

    public record CreateFacilityRequest(FacilityDto Facility);

    public record CreateFacilityResponse(Guid Id);

    public class CreateFacilityEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Facilities", async (CreateFacilityRequest request, ISender sender) =>
            {
                var command = new CreateFacilityCommand(request.Facility);

                var result = await sender.Send(command);

                var response = result.Adapt<CreateFacilityResponse>();

                return Results.Ok(response);

            })
                .WithName("CreateFacility")
                .Produces<CreateFacilityResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Facility")
                .WithDescription("Create Facility")
                ;
        }
    }
}
