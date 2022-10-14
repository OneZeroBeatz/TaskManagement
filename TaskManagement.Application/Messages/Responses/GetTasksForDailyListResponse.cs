using TaskManagement.Application.Messages.Responses.Dtos;

namespace TaskManagement.Application.Messages.Responses;

public class GetTasksForDailyListResponse
{
    public List<TaskDto> Tasks { get; private set; }

    public GetTasksForDailyListResponse(List<TaskDto> tasks)
    {
        Tasks = tasks;
    }
}