using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.Models
{
    public class RoomFacilities: Entity<Guid>
    {

        public Rooms Room { get; set; }

        public Guid RoomId { get; set; }

        public Facilities Facility { get; set; }

        public Guid FacilityId { get; set; }

        public RoomFacilities() { }

        public RoomFacilities(Guid roomId, Guid facilityId)
        {
            RoomId = roomId;
            FacilityId = facilityId;
        }

    }
}
