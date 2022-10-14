namespace TaskManagement.Infrastructure.Services
{
    public interface INotificationService
    {
        void AddOrUpdateNotificationTimezone(string userEmail, TimeZoneInfo timeZoneInfo);
    }
}