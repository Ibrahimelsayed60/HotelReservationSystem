using Notification.Data;
using Notification.Notifications.Service;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static MassTransit.ValidationResultExtensions;

namespace Notification.Notifications.Features.SendNotificationToEmail
{
    public record SendNotificationToEmailCommand(string toEmail, string subject, string body):ICommand<SendNotificationToEmailResult>;

    public record SendNotificationToEmailResult(bool IsSuccess);

    internal class SendNotificationToEmailHandler(NotificationDbContext dbContext, ISmtpService smtpService) : ICommandHandler<SendNotificationToEmailCommand, SendNotificationToEmailResult>
    {
        public async Task<SendNotificationToEmailResult> Handle(SendNotificationToEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await smtpService.SendAsync(request.toEmail, request.subject, request.body);
                return new SendNotificationToEmailResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send email: {ex.Message}");
            }
        }
    }
}
