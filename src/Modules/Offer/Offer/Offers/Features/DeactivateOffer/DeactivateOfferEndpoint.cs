using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Offer.Offers.Features.ActivateOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.DeactivateOffer
{
    public record DeactivateOfferRequest(Guid offerId);

    public record DeactivateOfferResponse(bool IsSuccess);

    public class DeactivateOfferEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/offers/{id}/deactivate", async (DeactivateOfferRequest request, ISender sender) =>
            {
                var result = await sender.Send(new DeactivateOfferCommand(request.offerId));

                var response = new DeactivateOfferResponse(result.IsSuccess);

                return Results.Ok(response);
            })
                .WithName("DeactivateOffer")
                .Produces<DeactivateOfferResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Deactivate Offer")
                .WithDescription("Deactivate Offer")
                ;
        }
    }
}
