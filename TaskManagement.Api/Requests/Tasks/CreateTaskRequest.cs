namespace TaskManagement.Api.Requests.Tasks
{
    public class CreateTaskRequest
    {
        public string Title { get;set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public int DailyListId { get; set; }
    }
}