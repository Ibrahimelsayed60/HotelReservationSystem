using Feedback.Feedbacks.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Data
{
    public class FeedbackDbContext:DbContext
    {

        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Feedbacks");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Feedback.Feedbacks.Models.Feedback> Feedbacks { get; set; }

        public DbSet<FeedbackReply> FeedbackReplies { get; set; }

        public DbSet<FeedbackTag> FeedbackTags { get; set; }

        public DbSet<FeedbackTagMapping> FeedbackTagMappings { get; set; }

    }
}
