using Feedback.Data;
using Feedback.Feedbacks.Dtos;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Features.GetMyFeedbacks
{
    public record GetMyFeedbacksQuery(Guid UserId):IQuery<GetMyFeedbacksResult>;

    public record GetMyFeedbacksResult(IEnumerable<FeedbackDto> Feedbacks);

    public class GetMyFeedbacksHandler(FeedbackDbContext dbContext) : IQueryHandler<GetMyFeedbacksQuery, GetMyFeedbacksResult>
    {
        public async Task<GetMyFeedbacksResult> Handle(GetMyFeedbacksQuery request, CancellationToken cancellationToken)
        {
            var feedbacks = await dbContext.Feedbacks
            .Where(f => f.UserId == request.UserId)
            .OrderByDescending(f => f.SubmittedAt)
            .Select(f => new FeedbackDto
            {
                Id = f.Id,
                ReservationId = f.ReservationId,
                Rating = f.Rating,
                Comment = f.Comment,
                SubmittedAt = f.SubmittedAt,
                IsResolved = f.IsResolved
            })
            .ToListAsync(cancellationToken);

            return new GetMyFeedbacksResult(feedbacks);
        }
    }
}
