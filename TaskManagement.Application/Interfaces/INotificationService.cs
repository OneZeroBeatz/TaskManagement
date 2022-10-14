namespace TaskManagement.Infrastructure.Services
{
    public interface INotificationService
    {
        void UpdateNotificationTimezone(string userEmail, TimeZoneInfo timeZoneInfo);
    }
}