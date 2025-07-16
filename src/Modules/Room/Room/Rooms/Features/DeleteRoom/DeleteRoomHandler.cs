using MassTransit;
using Room.Data;
using Room.Rooms.Models;
using Shared.Contracts.CQRS;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Room.Rooms.Features.DeleteRoom
{

    public record DeleteRoomCommand(Guid RoomId): ICommand<DeleteRoomResult>;

    public record DeleteRoomResult(bool IsSuccess);

    public class DeleteRoomHandler(RoomDbContext dbContext, IBus bus) : ICommandHandler<DeleteRoomCommand, DeleteRoomResult>
    {
        public async Task<DeleteRoomResult> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await dbContext.Rooms.FindAsync(request.RoomId, cancellationToken);

            if(room == null)
            {
                throw new Exception($"Room not found: {request.RoomId}");
            }

            DeleteExistedRoom(room);

            dbContext.Rooms.Update(room);

            await dbContext.SaveChangesAsync();

            var integrationEvent = new RoomDeletedIntegrationEvent
            {
                RoomId = room.Id
            };

            await bus.Publish(integrationEvent, cancellationToken);

            return new DeleteRoomResult(true);

        }

        private void DeleteExistedRoom(Models.Rooms room)
        {
            room.DeleteRoom(room.Id);
        }
    }
}
