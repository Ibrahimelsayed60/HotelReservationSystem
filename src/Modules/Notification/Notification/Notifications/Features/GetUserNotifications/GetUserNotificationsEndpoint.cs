using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Notification.Notifications.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Features.GetUserNotifications
{

    public record GetUserNotificationsResponse(IEnumerable<NotificationDto> Notifications);

    public class GetUserNotificationsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/notifications/usernotification/{userid}", async (Guid userid, ISender sender) =>
            {
                var result = await sender.Send(new GetUserNotificationsQuery(userid));

                var response = new GetUserNotificationsResponse(result.Notifications);

                return Results.Ok(response);
            })
                .WithName("GetUserNotifications")
                .Produces<GetUserNotificationsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("GetUserNotifications")
                .WithDescription("GetUserNotifications")
                ;
        }
    }
}
