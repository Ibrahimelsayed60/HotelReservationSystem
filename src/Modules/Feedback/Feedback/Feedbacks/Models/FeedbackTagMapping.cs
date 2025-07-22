using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Models
{
    public class FeedbackTagMapping:Entity<Guid>
    {

        public Guid FeedbackId { get; set; }
        public Feedback Feedback { get; set; }
        public Guid TagId { get; set; }

        public FeedbackTag Tag { get; set; }

    }
}
