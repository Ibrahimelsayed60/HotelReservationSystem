using Microsoft.EntityFrameworkCore;
using Reporting.Reportings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Data
{
    public class ReportingDbContext:DbContext
    {

        public ReportingDbContext(DbContextOptions<ReportingDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Reporting");



            base.OnModelCreating(modelBuilder);
        }

        public DbSet<DailyRevenueReport> DailyRevenueReports { get; set; }

        public DbSet<FeedbackReport> FeedbackReports { get; set; }

        public DbSet<OfferUsageReport> OfferUsageReports { get; set; }

        public DbSet<ReservationReport> ReservationReports { get; set; }

        public DbSet<RoomOccupancyReport> RoomOccupancyReports { get; set; }

        public DbSet<UserActivityReport> UserActivityReports { get; set; }
    }
}
