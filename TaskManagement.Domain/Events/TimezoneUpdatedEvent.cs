using MediatR;

namespace TaskManagement.Domain.Events
{
    public class TimezoneUpdatedEvent : INotification
    {
        public string UserEmail { get; set; } = string.Empty;
        public TimeZoneInfo Timezone { get; set; } = TimeZoneInfo.Local;
    }
}