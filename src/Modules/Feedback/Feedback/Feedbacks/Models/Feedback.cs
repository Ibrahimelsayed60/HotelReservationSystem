using Feedback.Feedbacks.Events;
using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Models
{
    public class Feedback:Aggregate<Guid>
    {
        public Guid ReservationId { get; private set; }
        public Guid UserId { get; private set; }
        public int Rating { get; private set; }
        public string? Comment { get; private set; }
        public DateTime SubmittedAt { get; private set; }
        public bool IsResolved { get; private set; }

        public static Feedback Submit(Guid reservationId, Guid userId, int rating, string? comment)
        {
            var feedback = new Feedback
            {
                Id = Guid.NewGuid(),
                ReservationId = reservationId,
                UserId = userId,
                Rating = rating,
                Comment = comment,
                SubmittedAt = DateTime.UtcNow,
                IsResolved = false
            };

            feedback.AddDomainEvent(new FeedbackSubmittedDomainEvent(feedback.Id));
            return feedback;
        }

        public void MarkAsResolved()
        {
            IsResolved = true;

            this.AddDomainEvent(new FeedbackResolvedEvent(Id));
        }

    }
}
