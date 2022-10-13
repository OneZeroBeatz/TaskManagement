using TaskManagement.Application.Messages.Responses.Dtos;

namespace TaskManagement.Api.Controllers
{
    public class GetTasksForDailyListResponse
    {
        public List<TaskDto> Tasks { get; private set; }

        public GetTasksForDailyListResponse(List<TaskDto> tasks)
        {
            Tasks = tasks;
        }
    }

}