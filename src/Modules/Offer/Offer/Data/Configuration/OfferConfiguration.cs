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
    public class OfferConfiguration : IEntityTypeConfiguration<Offer.Offers.Models.Offer>
    {
        public void Configure(EntityTypeBuilder<Offers.Models.Offer> builder)
        {
            builder.Property(o => o.DiscountPercentage).HasColumnType("decimal(18,2)");

            

        }
    }
}
