namespace TaskManagement.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string TimezoneId { get; set; } = TimeZoneInfo.Utc.Id;
    public List<DailyList> DailyLists { get; set; } = new();
}
