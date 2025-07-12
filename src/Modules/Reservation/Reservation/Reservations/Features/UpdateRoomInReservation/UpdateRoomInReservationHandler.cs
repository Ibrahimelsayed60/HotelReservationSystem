using Microsoft.EntityFrameworkCore;
using Reservation.Data;
using Shared.Contracts.CQRS;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Reservation.Reservations.Features.UpdateRoomInReservation
{
    public record UpdateRoomInReservationCommand(RoomUpdatedEventIntegrationEvent RoomUpdatedEvent):ICommand<UpdateRoomInReservationResult>;

    public record UpdateRoomInReservationResult(bool IsSuccess);

    public class UpdateRoomInReservationHandler(ReservationDbContext dbContext) 
        : ICommandHandler<UpdateRoomInReservationCommand, UpdateRoomInReservationResult>
    {
        public async Task<UpdateRoomInReservationResult> Handle(UpdateRoomInReservationCommand request, CancellationToken cancellationToken)
        {
            var itemsToUpdate = await dbContext.
                Reservations
                .Where(x=> x.RoomId == request.RoomUpdatedEvent.RoomId && x.CheckInDate >= DateTime.UtcNow)
                .ToListAsync(cancellationToken);

            if (!itemsToUpdate.Any())
            {
                return new UpdateRoomInReservationResult(false);
            }

            foreach (var item in itemsToUpdate)
            {
                //item.UpdatePrice(command.Price);
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateRoomInReservationResult(true);

        }
    }
}
