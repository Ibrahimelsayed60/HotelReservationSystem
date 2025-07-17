using Microsoft.EntityFrameworkCore;
using Offer.Offers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Data
{
    public class OfferDbContext:DbContext
    {

        public OfferDbContext(DbContextOptions<OfferDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Offers");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Offer.Offers.Models.Offer> Offers { get; set; }

        public DbSet<OfferRoom> OfferRooms { get; set; }

        public DbSet<OfferUsage> OfferUsages { get; set; }

    }
}
