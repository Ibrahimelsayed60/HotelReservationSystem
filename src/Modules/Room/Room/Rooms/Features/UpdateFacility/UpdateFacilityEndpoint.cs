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

namespace Room.Rooms.Features.UpdateFacility
{
    public record UpdateFacilityRequest(FacilityDto Facility);

    public record UpdateFacilityResponse(bool IsSuccess);

    public class UpdateFacilityEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/Facilities", async (UpdateFacilityRequest request, ISender sender) =>
            {
                var command = new UpdateFacilityCommand(request.Facility);

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateFacilityResponse>();

                return Results.Ok(response);

            })
                .WithName("UpdateFacility")
                .Produces<UpdateFacilityResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Update Facility")
                .WithSummary("Update Facility")
                ;
        }
    }
}
