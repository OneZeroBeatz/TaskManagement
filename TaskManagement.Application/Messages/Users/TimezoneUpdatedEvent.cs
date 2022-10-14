using MediatR;

namespace TaskManagement.Application.Messages.Users
{
    public class TimezoneUpdatedEvent : IRequest
    {
        public string UserEmail { get; set; } = string.Empty;
        public TimeZoneInfo Timezone { get; set; } = TimeZoneInfo.Local;
    }
}