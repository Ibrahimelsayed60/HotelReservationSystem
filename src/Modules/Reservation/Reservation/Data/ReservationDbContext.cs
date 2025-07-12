using Microsoft.EntityFrameworkCore;
using Reservation.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data
{
    public class ReservationDbContext: DbContext
    {

        public ReservationDbContext(DbContextOptions<ReservationDbContext> options):base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Reservations");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Reservation.Reservations.Models.Reservation> Reservations { get; set; }
    }
}
