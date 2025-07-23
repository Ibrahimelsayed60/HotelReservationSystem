using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Notification.Notifications.Features.MarkNotificationAsRead;
using Notification.Notifications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Features.DeleteNotification
{
    public record DeleteNotificationRequest(Guid NotificationId);

    public record DeleteNotificationResponse(bool IsSuccess);

    public class DeleteNotificationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/notificaton/delete/{notificationid}", async (Guid notificationid, ISender sender) =>
            {
                var result = await sender.Send(new DeleteNotificationCommand(notificationid));

                var response = new DeleteNotificationResponse(result.IsSuccess);

                return Results.Ok(response);
            })
                .WithName("DeleteNotification")
                .Produces<DeleteNotificationResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("DeleteNotification")
                .WithDescription("DeleteNotification")
                ;
        }
    }
}
