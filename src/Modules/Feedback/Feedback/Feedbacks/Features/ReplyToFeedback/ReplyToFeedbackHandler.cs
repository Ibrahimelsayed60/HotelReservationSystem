using Feedback.Data;
using Feedback.Feedbacks.Events;
using Feedback.Feedbacks.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.ValidationResultExtensions;

namespace Feedback.Feedbacks.Features.ReplyToFeedback
{

    public record ReplyToFeedbackCommand(Guid FeedbackId,Guid AdminId,string Message):ICommand<ReplyToFeedbackResult>;

    public record ReplyToFeedbackResult(Guid replyId);

    public class ReplyToFeedbackHandler(FeedbackDbContext dbContext) : ICommandHandler<ReplyToFeedbackCommand, ReplyToFeedbackResult>
    {
        public async Task<ReplyToFeedbackResult> Handle(ReplyToFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await dbContext.Feedbacks.FindAsync(new object[] { request.FeedbackId }, cancellationToken);
            if (feedback is null)
                throw new Exception("Feedback not found.");

            var reply = new FeedbackReply(request.FeedbackId, request.AdminId, request.Message);
            dbContext.FeedbackReplies.Add(reply);

            feedback.AddDomainEvent(new FeedbackRepliedEvent(feedback.Id, request.AdminId, request.Message));

            await dbContext.SaveChangesAsync(cancellationToken);

            return new ReplyToFeedbackResult(reply.Id);
        }
    }
}
