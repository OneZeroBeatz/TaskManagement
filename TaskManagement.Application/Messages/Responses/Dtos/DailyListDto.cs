namespace TaskManagement.Application.Messages.Responses.Dtos;

public class DailyListDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    //TODO: Consider using DateOnly
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
