namespace TaskManagement.Domain.Models;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public bool Done { get; set; }
    public int DailyListId { get; set; }
    public DailyList DailyList { get; set; }
}
