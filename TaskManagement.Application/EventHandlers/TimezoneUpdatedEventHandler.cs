using MediatR;
using TaskManagement.Domain.Events;
using TaskManagement.Infrastructure.Services;

namespace TaskManagement.Application.EventHandlers
{
    public class TimezoneUpdatedEventHandler : INotificationHandler<TimezoneUpdatedEvent>
    {
        private readonly INotificationService _notificationService;

        public TimezoneUpdatedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        public Task Handle(TimezoneUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _notificationService.AddOrUpdateNotificationTimezone(notification.UserEmail, notification.Timezone);
            return Task.CompletedTask;
        }
    }
}
