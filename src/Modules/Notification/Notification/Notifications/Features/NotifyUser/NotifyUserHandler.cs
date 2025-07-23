using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.ValidationResultExtensions;

namespace Notification.Notifications.Features.NotifyUser
{
    public record NotifyUserCommand(
        Guid UserId,
        string Title,
        string Message,
        string Type // Info, Success, Warning, Error
        ) :ICommand<NotifyUserResult>;
    
    public record NotifyUserResult(Guid NotificationId);

    public class NotifyUserHandler(NotificationDbContext dbContext) : ICommandHandler<NotifyUserCommand, NotifyUserResult>
    {
        public async Task<NotifyUserResult> Handle(NotifyUserCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification.Notifications.Models.Notification(
            request.UserId,
            request.Title,
            request.Message,
            request.Type);

            dbContext.Notifications.Add(notification);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new NotifyUserResult(notification.Id);
        }
    }
}
