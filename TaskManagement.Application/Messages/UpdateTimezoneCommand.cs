using MediatR;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages
{
    public class UpdateTimezoneCommand : IRequest<Result>
    {
        public string TimeZoneId { get; set; } = TimeZoneInfo.Utc.Id;
        public string UserEmail { get; set; } = string.Empty;
    }
}