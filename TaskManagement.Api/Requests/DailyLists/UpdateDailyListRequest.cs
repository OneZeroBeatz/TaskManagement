namespace TaskManagement.Api.Requests.DailyLists;

public class UpdateDailyListRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}