namespace TaskManagement.Api.Requests.Users;

public class UpdateTimezoneRequest
{
    public string TimeZoneId { get; set; } = TimeZoneInfo.Utc.Id;
}