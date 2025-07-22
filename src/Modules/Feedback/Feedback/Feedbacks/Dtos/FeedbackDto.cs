using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Dtos
{
    public class FeedbackDto
    {
        public Guid Id { get; set; }
        public Guid ReservationId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool IsResolved { get; set; }

    }
}
