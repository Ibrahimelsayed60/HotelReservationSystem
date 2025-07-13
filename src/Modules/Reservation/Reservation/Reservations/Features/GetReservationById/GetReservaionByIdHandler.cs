using Mapster;
using Microsoft.EntityFrameworkCore;
using Reservation.Data;
using Reservation.Reservations.Dtos;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Features.GetReservationById
{

    public record GetReservationByIdQuery(Guid Id):IQuery<GetReservationByIdResult>;

    public record GetReservationByIdResult(ReservationDto reservation);

    public class GetReservaionByIdHandler(ReservationDbContext dbContext) : IQueryHandler<GetReservationByIdQuery, GetReservationByIdResult>
    {
        public async Task<GetReservationByIdResult> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reserv = await dbContext.Reservations.Where(r => r.Id == request.Id).FirstOrDefaultAsync();


            if (reserv == null)
            {
                throw new Exception("Reservation not found");
            }

            var reservation = reserv.Adapt<ReservationDto>();

            return new GetReservationByIdResult(reservation);

        }
    }
}
