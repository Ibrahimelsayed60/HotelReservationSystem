using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Events
{
    public record FeedbackRepliedEvent(Guid FeedbackId, Guid AdminId, string Message) :IDomainEvent;
    
}
