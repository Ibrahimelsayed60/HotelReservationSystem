using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Notification.Notifications.Models;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Features.GetUnreadCount
{
    public record GetUnreadCountQuery(Guid UserId):IQuery<GetUnreadCountResult>;

    public record GetUnreadCountResult(int notificationCount);

    public class GetUnreadCountHandler(NotificationDbContext dbContext) : IQueryHandler<GetUnreadCountQuery, GetUnreadCountResult>
    {
        public async Task<GetUnreadCountResult> Handle(GetUnreadCountQuery request, CancellationToken cancellationToken)
        {
            var notificationCount = await dbContext.Notifications
            .CountAsync(n => n.UserId == request.UserId && !n.IsRead, cancellationToken);

            return new GetUnreadCountResult(notificationCount);
        }
    }
}
