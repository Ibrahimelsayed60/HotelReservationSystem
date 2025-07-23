using Microsoft.EntityFrameworkCore;
using Notification.Notifications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Data
{
    public class NotificationDbContext:DbContext
    {

        public NotificationDbContext(DbContextOptions<NotificationDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Notifications");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Notification.Notifications.Models.Notification> Notifications { get; set; }

        public DbSet<NotificationChannel> NotificationChannels { get; set; }

        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }

    }
}
