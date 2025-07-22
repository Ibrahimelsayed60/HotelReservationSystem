using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Feedbacks.Features.ExportFeedbackToCSV
{
    public record ExportFeedbackToCSVResponse(Stream Stream);

    internal class ExportFeedbackToCSVEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/feedback/csv", async ([FromQuery] DateTime from, [FromQuery] DateTime to, ISender sender) =>
            {
                var result = await sender.Send(new ExportFeedbackToCsvQuery(from, to));

                var response = new ExportFeedbackToCSVResponse(result.Stream);

                return Results.Ok(response);
            })
                .WithName("ExportFeedbackToCSV")
                .Produces<ExportFeedbackToCSVResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("ExportFeedbackToCSV")
                .WithDescription("ExportFeedbackToCSV")
                ;
        }
    }
}
