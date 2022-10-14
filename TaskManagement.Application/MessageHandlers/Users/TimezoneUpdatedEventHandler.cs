using MediatR;
using TaskManagement.Application.Messages.Users;
using TaskManagement.Infrastructure.Services;

namespace TaskManagement.Application.MessageHandlers.Users
{
    public class TimezoneUpdatedEventHandler : IRequestHandler<TimezoneUpdatedEvent>
    {
        private readonly INotificationService _notificationService;

        public TimezoneUpdatedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        public Task<Unit> Handle(TimezoneUpdatedEvent request, CancellationToken cancellationToken)
        {
            _notificationService.UpdateNotificationTimezone(request.UserEmail, request.Timezone);
            return Task.FromResult(Unit.Value);
        }
    }
}
