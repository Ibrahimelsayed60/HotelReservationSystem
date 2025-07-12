using MassTransit;
using Room.Data;
using Room.Rooms.Dtos;
using Shared.Contracts.CQRS;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Room.Rooms.Features.UpdateRoom
{

    public record UpdateRoomCommand(RoomDto Room):ICommand<UpdateRoomResult>;

    public record UpdateRoomResult(bool IsSuccess);

    public class UpdateRoomHandler(RoomDbContext dbContext, IBus bus) : ICommandHandler<UpdateRoomCommand, UpdateRoomResult>
    {
        public async Task<UpdateRoomResult> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await dbContext.Rooms.FindAsync(request.Room.Id, cancellationToken);

            if (room == null)
            {
                throw new Exception($"Room not found {request.Room.Id}");
            }

            UpdateRoomWithNewValues(room, request.Room);

            dbContext.Rooms.Update(room);

            await dbContext.SaveChangesAsync(cancellationToken);

            var integrationEvent = new RoomUpdatedEventIntegrationEvent
            {
                RoomId = room.Id,
                Name = room.Name,
                Type = room.Type,
                Price = (decimal)room.Price,
                Description = room.Description,
                Capacity = room.Capacity,
                UpdatedAt = (DateTime)room.LastModified,

            };

            await bus.Send(integrationEvent, cancellationToken);

            return new UpdateRoomResult(true);
        }

        private void UpdateRoomWithNewValues(Models.Rooms room1, RoomDto room2)
        {
            room1.UpdateRoom(
                room2.Name,
                room2.Type,
                room2.Price,
                room2.Description,
                room2.Capacity,
                room2.IsAvailable
                );
        }
    }
}
