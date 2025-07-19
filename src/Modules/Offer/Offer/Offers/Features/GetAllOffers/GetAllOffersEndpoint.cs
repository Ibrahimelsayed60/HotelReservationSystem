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

namespace Offer.Offers.Features.GetAllOffers
{

    public record GetAllOffersResponse(IEnumerable<OfferDto> Offers);

    public class GetAllOffersEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/offers/", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllOffersQuery());

                var response = new GetAllOffersResponse(result.Offers);

                return Results.Ok(response);
            })
                .WithName("GetAllOffers")
                .Produces<GetAllOffersResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get All Offers")
                .WithDescription("Get All Offers")
                ;
        }
    }
}
