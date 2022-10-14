namespace TaskManagement.Api.Requests.Users;

public class UpdateTimezoneRequest
{
    /// <summary>
    /// Represents id of timezone that user want to set. One of timezones can be get from TimeZoneInfo class
    /// </summary>
    public string TimezoneId { get; set; } = TimeZoneInfo.Utc.Id;
}