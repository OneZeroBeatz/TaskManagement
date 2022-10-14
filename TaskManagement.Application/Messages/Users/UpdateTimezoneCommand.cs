using MediatR;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages.Users;

public class UpdateTimezoneCommand : IRequest<Result>
{
    public string TimeZoneId { get; set; } = TimeZoneInfo.Utc.Id;
    public int UserId { get; set; }
}