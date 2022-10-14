using TaskManagement.Application.Dtos;

namespace TaskManagement.Application.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
