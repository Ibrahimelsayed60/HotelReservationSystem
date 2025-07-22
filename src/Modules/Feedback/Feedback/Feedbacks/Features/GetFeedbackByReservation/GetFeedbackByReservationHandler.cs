using Feedback.Data;
using Feedback.Feedbacks.Dtos;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Features.GetFeedbackByReservation
{
    public record GetFeedbackByReservationQuery(Guid ReservationId):IQuery<GetFeedbackByReservationResult>;

    public record GetFeedbackByReservationResult(FeedbackDto Feedback);

    public class GetFeedbackByReservationHandler(FeedbackDbContext dbContext) : IQueryHandler<GetFeedbackByReservationQuery, GetFeedbackByReservationResult>
    {
        public async Task<GetFeedbackByReservationResult> Handle(GetFeedbackByReservationQuery request, CancellationToken cancellationToken)
        {
            var feedback = await dbContext.Feedbacks
            .Where(f => f.ReservationId == request.ReservationId)
            .Select(f => new FeedbackDto
            {
                Id = f.Id,
                ReservationId = f.ReservationId,
                Rating = f.Rating,
                Comment = f.Comment,
                SubmittedAt = f.SubmittedAt,
                IsResolved = f.IsResolved
            })
            .FirstOrDefaultAsync(cancellationToken);

            return new GetFeedbackByReservationResult(feedback);
        }
    }
}
