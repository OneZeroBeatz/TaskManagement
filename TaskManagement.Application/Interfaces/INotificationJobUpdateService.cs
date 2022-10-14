namespace TaskManagement.Infrastructure.Services
{
    public interface INotificationJobUpdateService
    {
        void UpdateJob(string userEmail, TimeZoneInfo timeZoneInfo);
    }
}