namespace TaskManagement.Domain.Models;

public class DailyList
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    //TODO: Consider using DateOnly
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Task> Tasks { get; set; } = new();
}
