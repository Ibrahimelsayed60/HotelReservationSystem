using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Offer.Offers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Data.Configuration
{
    public class OfferRoomConfiguration : IEntityTypeConfiguration<OfferRoom>
    {
        public void Configure(EntityTypeBuilder<OfferRoom> builder)
        {
            builder.HasOne(or => or.Offer).WithMany(o => o.OfferRooms).HasForeignKey(o => o.OfferId);
        }
    }
}
