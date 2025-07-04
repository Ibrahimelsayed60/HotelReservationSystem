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
    public record GetAllRoomsQuery():IQuery<GetAllProductsResult>;

    public record GetAllProductsResult(IEnumerable<RoomDto> rooms);

    public class GetAllRoomsHandler(RoomDbContext dbContext) : IQueryHandler<GetAllRoomsQuery, GetAllProductsResult>
    {
        public async Task<GetAllProductsResult> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await dbContext.Rooms.AsNoTracking().OrderBy(r => r.Name).ToListAsync(cancellationToken);

            var roomsDto = rooms.Adapt<List<RoomDto>>();

            return new GetAllProductsResult(roomsDto);
        }
    }
}
