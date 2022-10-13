namespace TaskManagement.Api.Controllers
{
    public class GetTasksForDailyListResponse
    {
        public List<Domain.Models.Task> Tasks { get; set; }
    }
}