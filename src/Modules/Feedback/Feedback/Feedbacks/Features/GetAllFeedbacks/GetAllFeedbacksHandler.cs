using Feedback.Data;
using Feedback.Feedbacks.Dtos;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Features.GetAllFeedbacks
{
    public record GetAllFeedbacksQuery():IQuery<GetAllFeedbacksResult>;

    public record GetAllFeedbacksResult(IEnumerable<FeedbackDto> Feedbacks);

    public class GetAllFeedbacksHandler(FeedbackDbContext dbContext) : IQueryHandler<GetAllFeedbacksQuery, GetAllFeedbacksResult>
    {
        public async Task<GetAllFeedbacksResult> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
        {
            var feedbacks = await dbContext.Feedbacks
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

            return new GetAllFeedbacksResult(feedbacks);
        }
    }
}
