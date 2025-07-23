using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.ValidationResultExtensions;

namespace Notification.Notifications.Features.MarkNotificationAsRead
{
    public record MarkNotificationAsReadCommand(Guid NotificationId):ICommand<MarkNotificationAsReadResult>;

    public record MarkNotificationAsReadResult(bool IsSuccess);

    public class MarkNotificationAsReadHandler(NotificationDbContext dbContext) : ICommandHandler<MarkNotificationAsReadCommand, MarkNotificationAsReadResult>
    {
        public async Task<MarkNotificationAsReadResult> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            var notification = await dbContext.Notifications.FindAsync(new object[] { request.NotificationId }, cancellationToken);

            if (notification is null)
                throw new Exception("Notification not found.");

            notification.MarkAsRead();
            await dbContext.SaveChangesAsync(cancellationToken);

            return new MarkNotificationAsReadResult(true);
        }
    }
}
