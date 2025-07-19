using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Offer.Offers.Dtos;
using Offer.Offers.Features.GetAllOffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.GetOffersForRoom
{

    public record GetOffersForRoomResponse(IEnumerable<OfferDto> Offers);

    public class GetOffersForRoomEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/offers/{roomId}", async (Guid roomId, ISender sender) =>
            {
                var result = await sender.Send(new GetOffersForRoomQuery(roomId));

                var response = new GetOffersForRoomResponse(result.Offers);

                return Results.Ok(response);

            })
                .WithName("GetOffersForRoom")
                .Produces<GetOffersForRoomResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Offers For Room")
                .WithDescription("Get Offers For Room")
                ;
        }
    }
}
