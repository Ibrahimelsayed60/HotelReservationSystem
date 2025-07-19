using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Offer.Offers.Features.GetOffersForRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.RemoveOfferFromRoom
{

    public record RemoveOfferFromRoomRequest(Guid OfferId, Guid RoomId);

    public record RemoveOfferFromRoomResponse(bool IsSuccess);

    public class RemoveOfferFromRoomEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}/rooms/{roomId}", async (Guid id, Guid roomId, ISender sender) =>
            {
                var result = await sender.Send(new RemoveOfferFromRoomCommand(id, roomId));
                
                var response = new RemoveOfferFromRoomResponse(result.IsSuccess);
                
                return response;
            })
                .WithName("RemoveOfferFromRoom")
                .Produces<RemoveOfferFromRoomResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Remove Offer From Room")
                .WithDescription("Remove Offer From Room")
                ;
        }
    }
}
