using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using MailKit.Net.Smtp;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace Notification.Notifications.Service
{
    public class SmtpService : ISmtpService
    {
        private readonly IConfiguration _configuration;

        public SmtpService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(string toEmail, string subject, string body)
        {
            //var Client = new SmtpClient("smtp.gmail.com", 587);
            //Client.EnableSsl = true;
            //Client.Credentials = new NetworkCredential("ibrahimelsayed3015@gmail.com", "jiqqqtzeiyeufhoh");
            //Client.Send("ibrahimelsayed3015@gmail.com", toEmail, subject, body);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Smtp:From"]));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_configuration["Smtp:Host"], int.Parse(_configuration["Smtp:Port"]), true);
            await smtp.AuthenticateAsync(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
