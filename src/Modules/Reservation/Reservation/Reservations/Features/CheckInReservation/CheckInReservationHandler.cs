using Microsoft.EntityFrameworkCore;
using Reservation.Data;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.ValidationResultExtensions;

namespace Reservation.Reservations.Features.CheckInReservation
{
    public record CheckInReservationCommand(Guid ReservationId):ICommand<CheckInReservationResult>;

    public record CheckInReservationResult(bool IsSuccess);

    public class CheckInReservationHandler(ReservationDbContext dbContext) : ICommandHandler<CheckInReservationCommand, CheckInReservationResult>
    {
        public async Task<CheckInReservationResult> Handle(CheckInReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await dbContext.Reservations.FindAsync(new object[] { request.ReservationId }, cancellationToken);

            if (reservation is null)
                throw new Exception("Reservation not found.");

            try
            {
                reservation.CheckIn();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CheckInReservationResult(true);
        }
    }
}
