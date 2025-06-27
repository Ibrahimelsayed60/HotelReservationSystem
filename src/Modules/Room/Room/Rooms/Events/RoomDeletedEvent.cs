using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.Events
{
    public record RoomDeletedEvent(Models.Rooms room):IDomainEvent;
    
}
