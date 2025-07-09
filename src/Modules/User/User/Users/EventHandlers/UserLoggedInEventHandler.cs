using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Users.Events;

namespace User.Users.EventHandlers
{
    public class UserLoggedInEventHandler(ILogger<UserLoggedInEventHandler> logger) : INotificationHandler<UserLoggedInEvent>
    {
        public Task Handle(UserLoggedInEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
