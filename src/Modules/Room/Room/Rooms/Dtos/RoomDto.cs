using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.Dtos
{
    public record RoomDto(
        Guid Id,
        string Name,
        string Type,
        double Price,
        string Description,
        int Capacity,
        bool IsAvailable
        );
    
}
