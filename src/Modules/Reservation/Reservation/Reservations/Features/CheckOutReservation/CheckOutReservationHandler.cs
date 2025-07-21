using MassTransit;
using Reservation.Data;
using Reservation.Reservations.Features.CheckInReservation;
using Shared.Contracts.CQRS;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Features.CheckOutReservation
{
    public record CheckOutReservationCommand(Guid ReservationId):ICommand<CheckOutReservationResult>;

    public record CheckOutReservationResult(bool IsSuccess);

    public class CheckOutReservationHandler(ReservationDbContext dbContext, IBus bus) : ICommandHandler<CheckOutReservationCommand, CheckOutReservationResult>
    {
        public async Task<CheckOutReservationResult> Handle(CheckOutReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await dbContext.Reservations.FindAsync(new object[] { request.ReservationId }, cancellationToken);

            if (reservation is null)
                throw new Exception("Reservation not found.");

            try
            {
                reservation.CheckOut();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            await bus.Send(new ReservationCheckedOutIntegrationEvent
            {
                ReservationId = reservation.Id,
                RoomId = reservation.RoomId,
                RoomName = reservation.RoomName,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                TotalAmount = (decimal)reservation.TotalPriceIfOfferExistOrNot,
            }, cancellationToken);

            return new CheckOutReservationResult(true);
        }
    }
}
