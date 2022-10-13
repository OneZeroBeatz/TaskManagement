namespace TaskManagement.Api.Requests.Tasks
{
    public class UpdateTaskDoneStatusRequest
    {
        public bool Done { get; set; } = false;
    }
}