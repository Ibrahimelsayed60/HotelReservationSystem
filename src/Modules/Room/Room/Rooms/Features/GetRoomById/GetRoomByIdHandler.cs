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

namespace Room.Rooms.Features.GetRoomById
{

    public record GetRoomByIdQuery(Guid Id):IQuery<GetRoomByIdResult>;

    public record GetRoomByIdResult(RoomDto room);

    public class GetRoomByIdHandler(RoomDbContext dbContext) : IQueryHandler<GetRoomByIdQuery, GetRoomByIdResult>
    {
        public async Task<GetRoomByIdResult> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await dbContext.Rooms.AsNoTracking().SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if(room is null)
            {
                throw new Exception("Room not found");
            }

            var roomDto = room.Adapt<RoomDto>();

            return new GetRoomByIdResult(roomDto);

        }
    }
}
