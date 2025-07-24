using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Notification.Notifications.Features.NotifyUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Features.SendNotificationToEmail
{
    public record SendNotificationToEmailRequest(string toEmail, string subject, string body);

    public record SendNotificationToEmailResponse(bool IsSuccess);

    public class SendNotificationToEmailEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/notification/send", async (SendNotificationToEmailRequest request, ISender sender) =>
            {
                var result = await sender.Send(new SendNotificationToEmailCommand(request.toEmail, request.subject, request.body));

                var response = new SendNotificationToEmailResponse(result.IsSuccess);

                return Results.Ok(response);
            })
                .WithName("SendNotificationToEmail")
                .Produces<SendNotificationToEmailResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("SendNotificationToEmail")
                .WithDescription("SendNotificationToEmail")
                ;
        }
    }
}
