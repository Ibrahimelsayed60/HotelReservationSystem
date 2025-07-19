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

namespace Offer.Offers.Features.ApplyOfferToRoom
{

    public record ApplyOfferToRoomRequest(Guid OfferId, Guid RoomId, decimal? CustomDiscountPercentage);

    public record ApplyOfferToRoomResponse(bool IsSuccess);

    public class ApplyOfferToRoomEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/offers/{id}/rooms/{roomId}", async (Guid id, Guid roomId, decimal? customDiscountPercentage, ISender sender) =>
            {
                var result = await sender.Send(new ApplyOfferToRoomCommand(id, roomId, customDiscountPercentage));

                var response = new ApplyOfferToRoomResponse(true);

                return Results.Ok(response);
            })
                .WithName("ApplyOfferToRoom")
                .Produces<ApplyOfferToRoomResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Apply Offer To Room")
                .WithDescription("Apply Offer To Room")
                ;
        }
    }
}
