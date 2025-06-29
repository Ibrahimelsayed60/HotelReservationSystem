using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Room.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Data.Configuration
{
    public class RoomImagesConfiguration : IEntityTypeConfiguration<RoomImages>
    {
        public void Configure(EntityTypeBuilder<RoomImages> builder)
        {
            builder.HasOne(r => r.Room).WithMany(r => r.RoomImages).HasForeignKey(r => r.RoomId);
        }
    }
}
