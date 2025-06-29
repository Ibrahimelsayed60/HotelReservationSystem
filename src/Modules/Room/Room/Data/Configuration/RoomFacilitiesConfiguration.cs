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
    public class RoomFacilitiesConfiguration : IEntityTypeConfiguration<RoomFacilities>
    {
        public void Configure(EntityTypeBuilder<RoomFacilities> builder)
        {
            builder.HasOne(r => r.Room).WithMany(r => r.RoomFacilities).HasForeignKey(r => r.RoomId);

            builder.HasOne(r => r.Facility).WithMany(r => r.RoomFacilities).HasForeignKey(r => r.FacilityId);
        }
    }
}
