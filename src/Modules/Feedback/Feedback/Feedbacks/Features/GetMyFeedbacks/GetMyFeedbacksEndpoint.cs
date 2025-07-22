using Carter;
using Feedback.Feedbacks.Dtos;
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

namespace Feedback.Feedbacks.Features.GetMyFeedbacks
{

    public record GetMyFeedbacksResponse(IEnumerable<FeedbackDto> Feedbacks);

    internal class GetMyFeedbacksEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/feedback/myfeedbacks/{userid}", async (Guid userid, ISender sender) =>
            {
                var result = await sender.Send(new GetMyFeedbacksQuery(userid));
                var response = new GetMyFeedbacksResponse(result.Feedbacks);

                return Results.Ok(response);
            })
                .WithName("GetMyFeedbacks")
                .Produces<GetMyFeedbacksResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get MyFeedbacks")
                .WithDescription("Get MyFeedbacks")
                ;
        }
    }
}
