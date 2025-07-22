using Carter;
using Feedback.Feedbacks.Features.SubmitFeedback;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Features.ReplyToFeedback
{
    public record ReplyToFeedbackRequest(Guid FeedbackId, Guid AdminId, string Message);

    public record ReplyToFeedbackResponse(Guid ReplyId);
    public class ReplyToFeedbackEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/feedback/reply", async (ReplyToFeedbackRequest request, ISender sender) =>
            {
                var result = await sender.Send(new ReplyToFeedbackCommand(request.FeedbackId, request.AdminId, request.Message));
                var response = new ReplyToFeedbackResponse(result.replyId);
                return Results.Ok(response);
            })
                .WithName("ReplyToFeedback")
                .Produces<ReplyToFeedbackResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Reply To Feedback")
                .WithDescription("Reply To Feedback")
                ;
        }
    }
}
