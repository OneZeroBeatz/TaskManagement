namespace TaskManagement.Api.Requests
{
    public class UpdateTimezoneRequest
    {
        public string TimeZoneId { get; set; } = TimeZoneInfo.Utc.Id;
    }
}