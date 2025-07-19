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

namespace Offer.Offers.Features.UpdateOffer
{
    public record UpdateOfferRequest(OfferDto Offer);

    public record UpdateOfferResponse(bool IsSuccess);

    public class UpdateOfferEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/api/offers/{id}", async (UpdateOfferRequest request, ISender sender) =>
            {
                var result = await sender.Send(new UpdateOfferCommand(request.Offer));

                var response = new UpdateOfferResponse(result.IsSuccess);

                return response;
            })
                .WithName("UpdateOffer")
                .Produces<UpdateOfferResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Offer")
                .WithDescription("Update Offer")
                ;
        }
    }
}
