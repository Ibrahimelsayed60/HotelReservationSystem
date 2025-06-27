using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.Models
{
    public class RoomImages: Entity<Guid>
    {

        public string Url { get; set; }

        public Rooms Room { get; set; }

        public Guid RoomId { get; set; }

        public RoomImages() { }

        public RoomImages(string url, Guid imageId, Guid roomId)
        {
            Url = url;
            Id = imageId;
            RoomId = roomId;
        }
    }
}
