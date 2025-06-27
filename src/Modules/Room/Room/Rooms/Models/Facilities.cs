using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.Models
{
    public class Facilities : Entity<Guid>
    {

        public string Name { get; set; }

        public List<RoomFacilities> RoomFacilities { get; set; }

    }
}
