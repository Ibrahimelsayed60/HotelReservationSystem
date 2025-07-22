using Carter;
using Feedback.Feedbacks.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Features.ResolveFeedback
{
    public record ResolveFeedbackResponse(bool IsSuccess);

    public class ResolveFeedbackEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/feedback/resolve/{feedbackid}", async (Guid feedbackid, ISender sender) =>
            {
                var result = await sender.Send(new ResolveFeedbackCommand(feedbackid));

                var response = new ResolveFeedbackResponse(result.IsSuccess);

                return Results.Ok(response);
            })
                .WithName("ResolveFeedback")
                .Produces<ResolveFeedbackResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Resolve Feedback")
                .WithDescription("Resolve Feedback")
                ;
        }
    }
}
