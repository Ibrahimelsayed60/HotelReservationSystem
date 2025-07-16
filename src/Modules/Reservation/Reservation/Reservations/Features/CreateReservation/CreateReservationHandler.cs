using Reservation.Data;
using Reservation.Reservations.Dtos;
using Reservation.Reservations.Models;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Features.CreateReservation
{

    public record CreateReservationCommand(ReservationDto reservation):ICommand<CreateReservationResult>;

    public record CreateReservationResult(Guid Id);

    public class CreateReservationHandler(ReservationDbContext dbContext) : ICommandHandler<CreateReservationCommand, CreateReservationResult>
    {
        public async Task<CreateReservationResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = CreateNewReservarion(request.reservation);

            dbContext.Add(reservation);

            await dbContext.SaveChangesAsync();

            return new CreateReservationResult(reservation.Id);
        }

        private Reservation.Reservations.Models.Reservation CreateNewReservarion(ReservationDto reservation) 
        {
            var reservationCreated = Reservation.Reservations.Models.Reservation.Create(
                reservation.UserId,
                reservation.RoomId,
                reservation.CheckInDate,
                reservation.CheckOutDate,
                reservation.RoomName,
                reservation.RoomType,
                reservation.TotalPrice
                );
            return reservationCreated;
        }


    }
}
