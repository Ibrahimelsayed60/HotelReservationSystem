using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Notification.Notifications.Features.GetUserNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Features.GetUnreadCount
{
    public record GetUnreadCountResponse(int notificationCount);

    internal class GetUnreadCountEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/notification/unreadcount/{userid}", async (Guid userid,ISender sender) =>
            {
                var result = await sender.Send(new GetUnreadCountQuery(userid));

                var response = new GetUnreadCountResponse(result.notificationCount);

                return response;
            })
                .WithName("GetUnreadCount")
                .Produces<GetUnreadCountResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("GetUnreadCount")
                .WithDescription("GetUnreadCount")
                ;
        }
    }
}
