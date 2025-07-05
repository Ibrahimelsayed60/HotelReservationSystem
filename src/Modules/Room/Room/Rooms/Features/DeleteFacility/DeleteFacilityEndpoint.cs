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

namespace Room.Rooms.Features.DeleteFacility
{
    public record DeleteFacilityResponse(bool IsSuccess);

    public class DeleteFacilityEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Facilities/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteFacilityCommand(id);

                var result = await sender.Send(command);

                var response = result.Adapt<DeleteFacilityResponse>();

                return Results.Ok(response);
            })
                .WithName("DeleteFacility")
                .Produces<DeleteFacilityResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Facility")
                .WithDescription("Delete Facility");
        }
    }
}
