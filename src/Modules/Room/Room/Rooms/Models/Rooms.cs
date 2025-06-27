using Room.Rooms.Events;
using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.Models
{
    public class Rooms : Aggregate<Guid>
    {

        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Capacity { get; set; }

        public bool IsAvailable { get; set; }

        public List<RoomImages> RoomImages { get; set; }

        public List<RoomFacilities> RoomFacilities { get; set; }


        public static Rooms CreateRoom(Guid Id, string name, string type, decimal price, string description, int capacity, bool isAvailable)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var room = new Rooms() 
            {
                Id = Id,
                Name = name,
                Type = type,
                Price = price,
                Description = description,
                Capacity = capacity,
                IsAvailable = isAvailable
            };

            room.AddDomainEvent(new RoomCreatedEvent(room));

            return room;
        }

        public void UpdateRoom(string name, string type, decimal price, string description, int capacity, bool isAvailable)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);


            Name = name;
            Type = type;
            Description = description;
            Capacity = capacity;
            IsAvailable = isAvailable;
           
            if(Price != price)
            {
                Price = price;
                AddDomainEvent(new RoomPriceChangedEvent(this));
            }
        }

        public void DeleteRoom(Guid roomId)
        {

            Id = roomId;
            IsDeleted = true;

            AddDomainEvent(new RoomDeletedEvent(this));

        }


        public void AssignFacilityToRoom( Guid FacilityId)
        {
            var roomFacility = new RoomFacilities(this.Id, FacilityId);

            RoomFacilities.Add(roomFacility);

            AddDomainEvent(new RoomFacilityAssignedEvent(roomFacility));
            
        }

        public void AddImage(Guid imageId, string imageUrl)
        {
            var roomImage = new RoomImages(imageUrl, imageId, this.Id);

            RoomImages.Add(roomImage);

            AddDomainEvent(new RoomImageAddedEvent(roomImage));
        }

    }
}
