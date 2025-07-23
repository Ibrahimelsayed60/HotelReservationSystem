using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.ValidationResultExtensions;

namespace Notification.Notifications.Features.DeleteNotification
{
    public record DeleteNotificationCommand(Guid NotificationId):ICommand<DeleteNotificationResult>;

    public record DeleteNotificationResult(bool IsSuccess);

    public class DeleteNotificationHandler(NotificationDbContext dbContext) : ICommandHandler<DeleteNotificationCommand, DeleteNotificationResult>
    {
        public async Task<DeleteNotificationResult> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await dbContext.Notifications.FindAsync(new object[] { request.NotificationId }, cancellationToken);

            if (notification is null)
                throw new Exception("Notification not found.");

            dbContext.Notifications.Remove(notification);

            // Optional: domain event for audit
            //notification.AddDomainEvent(new NotificationDe(notification.Id));

            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteNotificationResult(true);
        }
    }
}
