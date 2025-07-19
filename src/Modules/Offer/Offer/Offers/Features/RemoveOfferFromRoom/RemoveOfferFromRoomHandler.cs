using Microsoft.EntityFrameworkCore;
using Offer.Data;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.RemoveOfferFromRoom
{
    public record RemoveOfferFromRoomCommand(Guid OfferId, Guid RoomId) :ICommand<RemoveOfferFromRoomResult>;

    public record RemoveOfferFromRoomResult(bool IsSuccess);

    public class RemoveOfferFromRoomHandler(OfferDbContext dbContext) : ICommandHandler<RemoveOfferFromRoomCommand, RemoveOfferFromRoomResult>
    {
        public async Task<RemoveOfferFromRoomResult> Handle(RemoveOfferFromRoomCommand request, CancellationToken cancellationToken)
        {
            var offer = await dbContext.Offers
            .Include(o => o.OfferRooms)
            .FirstOrDefaultAsync(o => o.Id == request.OfferId, cancellationToken);

            if (offer == null)
                throw new Exception("Offer not found.");

            try
            {
                offer.RemoveFromRoom(request.RoomId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
            return new RemoveOfferFromRoomResult(true);
        }
    }
}
