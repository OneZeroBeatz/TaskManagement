using MediatR;
using TaskManagement.Application.Messages.Users;
using TaskManagement.Infrastructure.Services;

namespace TaskManagement.Application.MessageHandlers.Users
{
    public class TimezoneUpdatedEventHandler : IRequestHandler<TimezoneUpdatedEvent>
    {
        private readonly INotificationJobUpdateService _notificationJobUpdateService;

        public TimezoneUpdatedEventHandler(INotificationJobUpdateService notificationJobUpdateService)
        {
            _notificationJobUpdateService = notificationJobUpdateService ?? throw new ArgumentNullException(nameof(notificationJobUpdateService));
        }

        public Task<Unit> Handle(TimezoneUpdatedEvent request, CancellationToken cancellationToken)
        {
            _notificationJobUpdateService.UpdateJob(request.UserEmail, request.Timezone);
            return Task.FromResult(Unit.Value);
        }
    }
}
