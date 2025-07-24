using Microsoft.EntityFrameworkCore;
using Payment.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Data
{
    public class PaymentDbContext:DbContext
    {

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Payments");

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<PaymentSession> PaymentSessions { get; set; }

        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        public DbSet<PaymentWebhookLog> PaymentWebhookLogs { get; set; }

        public DbSet<RefundTransaction> RefundTransactions { get; set; }

    }
}
