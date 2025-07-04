using Room.Data;
using Room.Rooms.Dtos;
using Room.Rooms.Models;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Room.Rooms.Features.CreateRoom
{
    public record CreateRoomCommand(RoomDto room):ICommand<CreateRoomResult>;
    public record CreateRoomResult(Guid Id);

    public class CreateRoomHandler(RoomDbContext dbContext) : ICommandHandler<CreateRoomCommand, CreateRoomResult>
    {
        public async Task<CreateRoomResult> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = CreateNewRoom(request.room);

            dbContext.Rooms.Add(room);

            await dbContext.SaveChangesAsync();

            return new CreateRoomResult(room.Id);
        }

        private Models.Rooms CreateNewRoom(RoomDto roomDto) 
        {
            var room = Models.Rooms.CreateRoom
                (
                Guid.NewGuid(),
                roomDto.Name,
                roomDto.Type,
                roomDto.Price,
                roomDto.Description,
                roomDto.Capacity,
                roomDto.IsAvailable
                );

            return room;
        }
    }
}
