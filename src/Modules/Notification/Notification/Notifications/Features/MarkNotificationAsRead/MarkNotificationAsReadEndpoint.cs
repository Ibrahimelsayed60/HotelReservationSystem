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

namespace Notification.Notifications.Features.MarkNotificationAsRead
{
    public record MarkNotificationAsReadRequest(Guid NotificationId);

    public record MarkNotificationAsReadResponse(bool IsSuccess);

    public class MarkNotificationAsReadEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/notification/markasread", async (MarkNotificationAsReadRequest request, ISender sender) =>
            {
                var result = await sender.Send(new MarkNotificationAsReadCommand(request.NotificationId));

                var response = new MarkNotificationAsReadResponse(result.IsSuccess);

                return Results.Ok(response);
            })
                .WithName("MarkNotificationAsRead")
                .Produces<MarkNotificationAsReadResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("MarkNotificationAsRead")
                .WithDescription("MarkNotificationAsRead")
                ;
        }
    }
}
