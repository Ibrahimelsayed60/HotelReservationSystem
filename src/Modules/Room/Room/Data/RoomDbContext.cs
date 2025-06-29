using Microsoft.EntityFrameworkCore;
using Room.Rooms.Models;

//using Room.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Room.Data
{
    public class RoomDbContext:DbContext
    {

        public RoomDbContext(DbContextOptions<RoomDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Rooms");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Room.Rooms.Models.Rooms> Rooms { get; set; }

        public DbSet<Facilities> Facilities { get; set; }

        public DbSet<RoomFacilities> RoomFacilities { get; set; }

        public DbSet<RoomImages> RoomImages { get; set; }

    }
}
