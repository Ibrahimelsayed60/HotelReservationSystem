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

namespace Reservation.Reservations.Features.GetReservationForUser
{
    public record GetReservationForUserQuery(Guid userId):IQuery<GetReservationForUserResult>;

    public record GetReservationForUserResult(IEnumerable<ReservationDto> reservations);

    public class GetReservationForUserHandler(ReservationDbContext dbContext) : IQueryHandler<GetReservationForUserQuery, GetReservationForUserResult>
    {
        public async Task<GetReservationForUserResult> Handle(GetReservationForUserQuery request, CancellationToken cancellationToken)
        {
            var reservations = await dbContext.Reservations.Where(r => r.UserId == request.userId).ToListAsync();

            var reservationsDto = reservations.Adapt<List<ReservationDto>>();

            return new GetReservationForUserResult(reservationsDto);
        }
    }
}
