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
    public class FacilityConfiguration : IEntityTypeConfiguration<Facilities>
    {
        public void Configure(EntityTypeBuilder<Facilities> builder)
        {
            builder.Property(r => r.Name).IsRequired();
        }
    }
}
