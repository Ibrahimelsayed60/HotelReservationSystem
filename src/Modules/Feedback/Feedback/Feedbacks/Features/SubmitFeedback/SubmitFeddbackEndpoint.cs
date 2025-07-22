using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Features.SubmitFeedback
{
    public record SubmitFeedbackRequest(Guid ReservationId, Guid UserId, int Rating, string? Comment);

    public record SubmitFeedbackResponse(Guid FeedbackId);

    public class SubmitFeddbackEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/feedback/submit", async (SubmitFeedbackRequest request, ISender sender) =>
            {
                var result = await sender.Send(new SubmitFeedbackCommand(request.ReservationId, request.UserId, request.Rating, request.Comment));
                var response = new SubmitFeedbackResponse(result.FeedbackId);

                return Results.Ok(response);
            })
                .WithName("SubmitFeedback")
                .Produces<SubmitFeedbackResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Submit Feedback")
                .WithDescription("Submit Feedback")
                ;
        }
    }
}
