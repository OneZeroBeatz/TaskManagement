using Invoicing.CrossCutting.Domain;
using MediatR;

namespace TaskManagement.Application.Messages
{
    public class UpdateTimezoneCommand : IRequest<Result>
    {
        public string TimeZoneId { get; set; } = TimeZoneInfo.Utc.Id;
        public string Email { get; set; } = string.Empty;
    }
}