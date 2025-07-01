using Room.Rooms.Models;
using Shared.Data.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Room.Data
{
    public class RoomContextSeed : IDataSeeder
    {
        private readonly RoomDbContext _dbContext;

        public RoomContextSeed(RoomDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedAllAsync()
        {
            if(_dbContext.Rooms.Count() == 0)
            {
                var roomsData = File.ReadAllText("D:/Projects/HotelReservationSystem/src/Modules/Room/Room/Data/DataSeed/Rooms.json");

                var rooms = JsonSerializer.Deserialize<List<Rooms.Models.Rooms>>(roomsData);

                if (rooms?.Count() > 0)
                {
                    foreach (var room in rooms)
                    {
                        _dbContext.Set<Rooms.Models.Rooms>().Add(room);
                    }
                    await _dbContext.SaveChangesAsync();
                }

            }


            if(_dbContext.RoomFacilities.Count() == 0)
            {
                var facilitiesData = File.ReadAllText("D:/Projects/HotelReservationSystem/src/Modules/Room/Room/Data/DataSeed/Facility.json");
                var facilities = JsonSerializer.Deserialize<List<Facilities>>(facilitiesData);

                if(facilities?.Count() > 0)
                {
                    foreach(var facility in facilities)
                    {
                        _dbContext.Set<Facilities>().Add(facility);
                    }

                    await _dbContext.SaveChangesAsync();
                }

            }

            if(_dbContext.RoomFacilities.Count() == 0)
            {
                var roomFacilityData = File.ReadAllText("D:/Projects/HotelReservationSystem/src/Modules/Room/Room/Data/DataSeed/RoomFacilities.json");
                var roomFacilities = JsonSerializer.Deserialize<List<RoomFacilities>>(roomFacilityData);

                if(roomFacilities?.Count() > 0)
                {
                    foreach(var roomFacility in roomFacilities)
                    {
                        _dbContext.Set<RoomFacilities>().Add(roomFacility);
                    }
                    await _dbContext.SaveChangesAsync();
                }

            }

        }
    }
}
