using Hangfire;
using TaskManagement.Application.Services;

namespace TaskManagement.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly IUserNotificationService _userNotificationService;

    public NotificationService(IUserNotificationService userNotificationService)
    {
        _userNotificationService = userNotificationService ?? throw new ArgumentNullException(nameof(userNotificationService));
    }

    public void AddOrUpdateNotificationTimezone(string userEmail, TimeZoneInfo timeZoneInfo)
    {
        RecurringJob.AddOrUpdate(userEmail, () => _userNotificationService.NotifyUser(userEmail), "0 0 * * *", timeZoneInfo);
    }
}
