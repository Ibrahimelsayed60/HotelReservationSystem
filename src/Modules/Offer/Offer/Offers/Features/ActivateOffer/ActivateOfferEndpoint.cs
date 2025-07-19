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

namespace Offer.Offers.Features.ActivateOffer
{
    public record ActivateOfferRequest(Guid offerId);

    public record ActivateOfferResponse(bool IsSuccess);

    public class ActivateOfferEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/offers/{id}/activate", async (ActivateOfferRequest request, ISender sender) =>
            {
                var result = await sender.Send(new ActivateOfferCommand(request.offerId));

                var response = new ActivateOfferResponse(result.IsSuccess);

                return Results.Ok(response);
            })
                .WithName("ActivateOffer")
                .Produces<ActivateOfferResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Activate Offer")
                .WithDescription("Activate Offer");
        }
    }
}
