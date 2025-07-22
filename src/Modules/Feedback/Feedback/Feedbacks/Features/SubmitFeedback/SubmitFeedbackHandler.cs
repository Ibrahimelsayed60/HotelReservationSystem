using Feedback.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static MassTransit.ValidationResultExtensions;

namespace Feedback.Feedbacks.Features.SubmitFeedback
{

    public record SubmitFeedbackCommand(Guid ReservationId,Guid UserId,int Rating,string? Comment) :ICommand<SubmitFeedbackResult>;

    public record SubmitFeedbackResult(Guid FeedbackId);

    public class SubmitFeedbackHandler(FeedbackDbContext dbContext) : ICommandHandler<SubmitFeedbackCommand, SubmitFeedbackResult>
    {
        public async Task<SubmitFeedbackResult> Handle(SubmitFeedbackCommand request, CancellationToken cancellationToken)
        {
            var existing = await dbContext.Feedbacks
            .AnyAsync(f => f.ReservationId == request.ReservationId, cancellationToken);

            if (existing)
                throw new Exception("Feedback already exists for this reservation.");

            var feedback = Feedback.Feedbacks.Models.Feedback.Submit(request.ReservationId, request.UserId, request.Rating, request.Comment);

            dbContext.Feedbacks.Add(feedback);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new SubmitFeedbackResult(feedback.Id);
        }
    }
}
