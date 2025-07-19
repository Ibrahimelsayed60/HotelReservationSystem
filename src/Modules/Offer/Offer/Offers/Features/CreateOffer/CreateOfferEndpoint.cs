using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Offer.Offers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.CreateOffer
{
    public record CreateOfferRequest(OfferDto Offer);

    public record CreateOfferResponse(Guid offerId);

    public class CreateOfferEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/offers/", async (CreateOfferRequest request, ISender sender) =>
            {
                var command = new CreateOfferCommand(request.Offer);

                var result = await sender.Send(command);

                var response = new CreateOfferResponse(result.offerId);

                return Results.Ok(response);
            })
                .WithName("CreateOffer")
                .Produces<CreateOfferResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Offer")
                .WithDescription("Create Offer");
        }
    }
}
