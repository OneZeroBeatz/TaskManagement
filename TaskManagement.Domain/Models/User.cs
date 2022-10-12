namespace TaskManagement.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string TimeZoneId { get; set; } = TimeZoneInfo.Utc.Id;
}
