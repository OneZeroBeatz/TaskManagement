using Hangfire;
using TaskManagement.Application.Services;

namespace TaskManagement.Infrastructure.Services;

public class NotificationJobUpdateService : INotificationJobUpdateService
{
    private readonly IUserNotificationService _userNotificationService;

    public NotificationJobUpdateService(IUserNotificationService userNotificationService)
    {
        _userNotificationService = userNotificationService ?? throw new ArgumentNullException(nameof(userNotificationService));
    }

    public void UpdateJob(string userEmail, TimeZoneInfo timeZoneInfo)
    {
        RecurringJob.AddOrUpdate(userEmail, () => _userNotificationService.NotifyUser(userEmail), "0 0 * * *", timeZoneInfo);
    }
}
