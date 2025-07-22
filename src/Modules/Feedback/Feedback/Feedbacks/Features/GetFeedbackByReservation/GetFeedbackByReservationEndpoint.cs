using Carter;
using Feedback.Feedbacks.Dtos;
using Feedback.Feedbacks.Features.GetMyFeedbacks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Features.GetFeedbackByReservation
{
    public record GetFeedbackByReservationResponse(FeedbackDto Feedback);

    public class GetFeedbackByReservationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/feedback/feedbackreservation/{reservationid}", async (Guid reservationid, ISender sender) =>
            {
                var result = await sender.Send(new GetFeedbackByReservationQuery(reservationid));

                var response = new GetFeedbackByReservationResponse(result.Feedback);

                return Results.Ok(response);
            })
                .WithName("GetFeedbackByReservation")
                .Produces<GetFeedbackByReservationResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("GetFeedbackByReservation")
                .WithDescription("GetFeedbackByReservation")
                ;
        }
    }
}
