using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Services;
using TaskManagement.Infrastructure.Configurations;

namespace TaskManagement.Infrastructure.Services
{
    public partial class EmailSender : IEmailSender
    {
        private readonly MailConfiguration _mailConfiguration;

        public EmailSender(MailConfiguration mailConfiguration)
        {
            _mailConfiguration = mailConfiguration ?? throw new ArgumentNullException(nameof(mailConfiguration));
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {

            var builder = new BodyBuilder
            {
                HtmlBody = mailRequest.Body
            };

            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailConfiguration.EmailFrom),
                Subject = mailRequest.Subject,
                Body = builder.ToMessageBody()
            };

            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

            using var smtp = new SmtpClient();
            smtp.Connect(_mailConfiguration.Host, _mailConfiguration.Port, SecureSocketOptions.None);
            smtp.Authenticate(_mailConfiguration.EmailFrom, _mailConfiguration.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
