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

namespace Offer.Offers.Features.GetOfferById
{

    public record GetOfferByIdResponse(OfferDto Offer);

    public class GetOfferByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/offers/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetOfferByIdQuery(id));

                var response = new GetOfferByIdResponse(result.Offer);

                return Results.Ok(response);
            })
                .WithName("GetOfferById")
                .Produces<GetOfferByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Offer By Id")
                .WithDescription("Get Offer By Id")
                ;
        }
    }
}
