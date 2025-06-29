using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Room.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Data.Configuration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room.Rooms.Models.Rooms>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Room.Rooms.Models.Rooms> builder)
        {
            builder.Property(r => r.Name).IsRequired();

            builder.Property(r => r.Capacity).IsRequired();

            builder.Property(r => r.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(r => r.Type).IsRequired();

            builder.Property(r => r.Description).IsRequired();

            builder.Property(r => r.IsAvailable).IsRequired();
        }
    }
}
