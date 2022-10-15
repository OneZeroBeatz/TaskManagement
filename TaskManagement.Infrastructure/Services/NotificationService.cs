using Hangfire;
using TaskManagement.Application.Services;
using TaskManagement.Infrastructure.Bridges;

namespace TaskManagement.Infrastructure.Services;

public class NotificationService : INotificationService
{
    public void AddOrUpdateNotificationTimezone(string userEmail, TimeZoneInfo timeZoneInfo)
    {
        var command = new SendCompletedTasksUserNotificationCommand() { UserEmail = userEmail };

        RecurringJob.AddOrUpdate<MediatorHangfireBrigde>(userEmail, x => x.Send(command), "0 0 * * *", timeZoneInfo);
    }
}
