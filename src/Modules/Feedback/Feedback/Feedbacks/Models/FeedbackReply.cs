﻿using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Models
{
    public class FeedbackReply:Entity<Guid>
    {
        public FeedbackReply(Guid feedbackId, Guid adminId, string message)
        {
            FeedbackId = feedbackId;
            AdminId = adminId;
            Message = message;
            RepliedAt = DateTime.Now;
        }

        public Guid FeedbackId { get; private set; }
        public Feedback Feedback { get; set; }
        public Guid AdminId { get; private set; }
        public string Message { get; private set; }
        public DateTime RepliedAt { get; private set; }

    }
}
