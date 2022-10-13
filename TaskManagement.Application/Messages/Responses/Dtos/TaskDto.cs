namespace TaskManagement.Application.Messages.Responses.Dtos
{
    public class TaskDto
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Deadline { get; private set; }
        public bool Done { get; private set; }
        public int DailyListId { get; private set; }

        public TaskDto(int id, string title, string description, DateTime deadline, bool done, int dailyListId)
        {
            Id = id;
            Title = title;
            Description = description;
            Deadline = deadline;
            Done = done;
            DailyListId = dailyListId;
        }
    }
}
