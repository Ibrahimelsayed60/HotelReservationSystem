using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Events
{
    public class FeedbackResolvedEvent(Guid FeedbackId):IDomainEvent;
    
}
