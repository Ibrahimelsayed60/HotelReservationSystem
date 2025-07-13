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

namespace Reservation.Reservations.Features.GetReservationsForRoom
{

    public record GetReservationsForRoomQuery(Guid roomId):IQuery<GetReservationForRoomResult>;

    public record GetReservationForRoomResult(IEnumerable<ReservationDto> reservations);

    public class GetReservationsForRoomHandler(ReservationDbContext dbContext) : IQueryHandler<GetReservationsForRoomQuery, GetReservationForRoomResult>
    {
        public async Task<GetReservationForRoomResult> Handle(GetReservationsForRoomQuery request, CancellationToken cancellationToken)
        {
            var reservations = await dbContext.Reservations.Where(r => r.RoomId == request.roomId).ToListAsync();

            var result = reservations.Adapt<List<ReservationDto>>();

            return new GetReservationForRoomResult(result);

        }
    }
}
