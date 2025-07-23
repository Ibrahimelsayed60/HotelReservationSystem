using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Notification.Notifications.Dtos;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Features.GetUserNotifications
{

    public record GetUserNotificationsQuery(Guid UserId):IQuery<GetUserNotificationsResult>;

    public record GetUserNotificationsResult(IEnumerable<NotificationDto> Notifications);

    public class GetUserNotificationsHandler(NotificationDbContext dbContext) : IQueryHandler<GetUserNotificationsQuery, GetUserNotificationsResult>
    {
        public async Task<GetUserNotificationsResult> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await dbContext.Notifications
            .Where(n => n.UserId == request.UserId)
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new NotificationDto
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            })
            .ToListAsync(cancellationToken);

            return new GetUserNotificationsResult(notifications);
        }
    }
}
