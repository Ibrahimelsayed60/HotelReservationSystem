using Mapster;
using Microsoft.EntityFrameworkCore;
using Room.Data;
using Room.Rooms.Dtos;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.Features.GetAllRooms
{
    public record GetAllRoomsQuery():IQuery<GetAllRoomsResult>;

    public record GetAllRoomsResult(IEnumerable<RoomDto> rooms);

    public class GetAllRoomsHandler(RoomDbContext dbContext) : IQueryHandler<GetAllRoomsQuery, GetAllRoomsResult>
    {
        public async Task<GetAllRoomsResult> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await dbContext.Rooms.AsNoTracking().OrderBy(r => r.Name).ToListAsync(cancellationToken);

            var roomsDto = rooms.Adapt<List<RoomDto>>();

            return new GetAllRoomsResult(roomsDto);
        }
    }
}
