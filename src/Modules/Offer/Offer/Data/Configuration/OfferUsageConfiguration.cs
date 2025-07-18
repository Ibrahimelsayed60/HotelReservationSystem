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
    public class OfferUsageConfiguration : IEntityTypeConfiguration<OfferUsage>
    {
        public void Configure(EntityTypeBuilder<OfferUsage> builder)
        {
            builder.Property(o => o.DiscountAmount).HasColumnType("deciaml(18,2)");

            builder.HasOne(ou => ou.Offer).WithMany(o => o.OfferUsages).HasForeignKey(ou => ou.OfferId);
        }
    }
}
