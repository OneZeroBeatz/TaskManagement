namespace TaskManagement.Api.Controllers
{
    public class UpdateTaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
    }
}