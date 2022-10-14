namespace TaskManagement.Application.Messages.Responses.Dtos;

public class DailyListDto
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime Date { get; private set; }

    public DailyListDto(int id, string title, string description, DateTime date)
    {
        Id = id;
        Title = title;
        Description = description;
        Date = date;
    }
}