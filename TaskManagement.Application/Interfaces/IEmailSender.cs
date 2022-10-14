using TaskManagement.Application.Dtos;

namespace TaskManagement.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
