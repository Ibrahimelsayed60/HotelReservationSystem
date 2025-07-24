using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Notifications.Service
{
    public interface ISmtpService
    {
        Task SendAsync(string toEmail, string subject, string body);
    }
}
