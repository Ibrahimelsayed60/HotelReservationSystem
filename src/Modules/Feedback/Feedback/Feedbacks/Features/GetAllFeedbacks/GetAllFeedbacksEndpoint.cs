using Carter;
using Feedback.Feedbacks.Dtos;
using Feedback.Feedbacks.Features.GetMyFeedbacks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Features.GetAllFeedbacks
{
    public record GetAllFeedbacksResponse(IEnumerable<FeedbackDto> Feedbacks);

    public class GetAllFeedbacksEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/feedback/all", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllFeedbacksQuery());

                var response = new GetAllFeedbacksResponse(result.Feedbacks);

                return Results.Ok(response);

            })
                .WithName("GetAllFeedbacks")
                .Produces<GetAllFeedbacksResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("GetAllFeedbacks")
                .WithDescription("GetAllFeedbacks")
                ;
        }
    }
}
