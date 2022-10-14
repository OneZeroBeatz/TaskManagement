namespace TaskManagement.Application.Services
{
    public interface IEmailSender
    {
        public void SendEmail(object emailDto);

    }
}
