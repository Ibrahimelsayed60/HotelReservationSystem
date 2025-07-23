using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Features.NotifyUser
{
    public record NotifyUserRequest(
        Guid UserId,
        string Title,
        string Message,
        string Type // Info, Success, Warning, Error
        );

    public record NotifyUserResponse(Guid NotificationId);

    public class NotifyUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/notification/notifyuser", async (NotifyUserRequest request, ISender sender) =>
            {
                var result = await sender.Send(new NotifyUserCommand(request.UserId, request.Title, request.Message, request.Type));

                var response = new NotifyUserResponse(result.NotificationId);

                return Results.Ok(response);
            })
                .WithName("NotifyUser")
                .Produces<NotifyUserResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("NotifyUser")
                .WithDescription("NotifyUser")
                ;
        }
    }
}
