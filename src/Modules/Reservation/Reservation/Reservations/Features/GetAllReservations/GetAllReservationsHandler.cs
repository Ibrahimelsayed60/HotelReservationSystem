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

namespace Reservation.Reservations.Features.GetAllReservations
{
    public record GetAllReservationsQuery():IQuery<GetAllReservationsResult>;

    public record GetAllReservationsResult(IEnumerable<ReservationDto> reservations);

    public class GetAllReservationsHandler(ReservationDbContext dbContext) : IQueryHandler<GetAllReservationsQuery, GetAllReservationsResult>
    {
        public async Task<GetAllReservationsResult> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await dbContext.Reservations.AsNoTracking().ToListAsync(cancellationToken);

            var reservationsDto = reservations.Adapt<List<ReservationDto>>();

            return new GetAllReservationsResult(reservationsDto);
        }
    }
}
