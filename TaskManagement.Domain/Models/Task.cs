namespace TaskManagement.Domain.Models;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    //TODO: Consider enumeration
    public bool Done { get; set; }
    public DateTime? LastDoneUpdate { get; set; }
    public int DailyListId { get; set; }
    public DailyList DailyList { get; set; } = null!;
}
