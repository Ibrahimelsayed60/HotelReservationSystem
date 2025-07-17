using MassTransit;
using Microsoft.EntityFrameworkCore;
using Reservation.Data;
using Shared.Contracts.CQRS;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static MassTransit.ValidationResultExtensions;

namespace Reservation.Reservations.Features.CancelReservation
{
    public record CancelReservarionCommand(Guid ReservarionId):ICommand<CancelReservationResult>;

    public record CancelReservationResult(bool IsSuccess);

    public class CancelReservationHandler(ReservationDbContext dbContext, IBus bus) : ICommandHandler<CancelReservarionCommand, CancelReservationResult>
    {
        public async Task<CancelReservationResult> Handle(CancelReservarionCommand request, CancellationToken cancellationToken)
        {
            var reservation = await dbContext.Reservations.FindAsync(new object[] { request.ReservarionId }, cancellationToken);

            if (reservation is null)
                throw new Exception("Reservation not found.");

            try
            {
                reservation.Cancel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            await bus.Send(new RoomReservationReleasedIntegrationEvent
            {
                ReservationId=reservation.Id,
                RoomId=reservation.RoomId,
                CheckInDate=reservation.CheckInDate,
                CheckOutDate=reservation.CheckOutDate
            }, cancellationToken);

            return new CancelReservationResult(true);
        }
    }
}
