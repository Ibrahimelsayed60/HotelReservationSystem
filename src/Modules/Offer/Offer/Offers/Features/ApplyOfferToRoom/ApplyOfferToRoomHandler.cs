using Microsoft.EntityFrameworkCore;
using Offer.Data;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.ApplyOfferToRoom
{

    public record ApplyOfferToRoomCommand(Guid OfferId, Guid RoomId, decimal? CustomDiscountPercentage):ICommand<ApplyOfferToRoomResult>;

    public record ApplyOfferToRoomResult(bool IsSuccess);

    public class ApplyOfferToRoomHandler(OfferDbContext dbContext) : ICommandHandler<ApplyOfferToRoomCommand, ApplyOfferToRoomResult>
    {
        public async Task<ApplyOfferToRoomResult> Handle(ApplyOfferToRoomCommand request, CancellationToken cancellationToken)
        {
            var offer = await dbContext.Offers
            .Include(o => o.OfferRooms)
            .FirstOrDefaultAsync(o => o.Id == request.OfferId, cancellationToken);

            if (offer is null)
                throw new Exception("Offer not found.");

            try
            {
                offer.ApplyToRoom(request.RoomId, request.CustomDiscountPercentage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApplyOfferToRoomResult(true);
        }
    }
}
