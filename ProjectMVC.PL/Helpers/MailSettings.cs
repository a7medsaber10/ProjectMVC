using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using ProjectMVC.DAL.Models;
using ProjectMVC.PL.Services.Settings;

namespace ProjectMVC.PL.Helpers
{
    public class MailSettings : IMailSettings
    {
        private Services.Settings.MailSettings _options;

        public MailSettings(IOptions<Services.Settings.MailSettings> options)
        {
            _options = options.Value;
        }
        public void SendEmail(Email email)
        {
            var mail = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_options.Email),
                Subject = email.Subject,
            };

            mail.To.Add(MailboxAddress.Parse(email.Recepients));

            mail.From.Add(MailboxAddress.Parse(_options.Email));

            var builder = new BodyBuilder();
            builder.TextBody = email.Body;

            mail.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_options.Host, _options.Port, SecureSocketOptions.StartTls);

            smtp.Authenticate(_options.Email, _options.Password);

            smtp.Send(mail);

            smtp.Disconnect(true);
        }
    }
}
