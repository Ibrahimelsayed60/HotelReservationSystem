using Feedback.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.ValidationResultExtensions;

namespace Feedback.Feedbacks.Features.ResolveFeedback
{
    public record ResolveFeedbackCommand(Guid FeedbackId):ICommand<ResolveFeedbackResult>;

    public record ResolveFeedbackResult(bool IsSuccess);

    public class ResolveFeedbackHandler(FeedbackDbContext dbContext) : ICommandHandler<ResolveFeedbackCommand, ResolveFeedbackResult>
    {
        public async Task<ResolveFeedbackResult> Handle(ResolveFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await dbContext.Feedbacks.FindAsync(new object[] { request.FeedbackId }, cancellationToken);

            if (feedback is null)
                throw new Exception("Feedback not found.");

            feedback.MarkAsResolved();
            await dbContext.SaveChangesAsync(cancellationToken);

            return new ResolveFeedbackResult(true);
        }
    }
}
