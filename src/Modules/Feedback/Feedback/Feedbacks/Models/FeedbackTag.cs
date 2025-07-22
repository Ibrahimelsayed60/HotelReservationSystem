using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Models
{
    public class FeedbackTag:Entity<Guid>
    {

        public string Name { get; set; }    // e.g., "Cleanliness", "Service", "Noise"

    }
}
