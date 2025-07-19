using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.DeleteOffer
{

    public record DeleteOfferResponse(bool IsSuccess);

    public class DeleteOfferEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/api/offers/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteOfferCommand(id));

                var response = new DeleteOfferResponse(result.IsSuccess);

                return Results.Ok(response);
            })
                .WithName("DeleteOffer")
                .Produces<DeleteOfferResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Offer")
                .WithDescription("Delete Offer")
                ;
        }
    }
}
