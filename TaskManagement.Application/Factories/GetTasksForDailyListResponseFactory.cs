using TaskManagement.Api.Controllers;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.Responses.Dtos;

namespace TaskManagement.Application.Factories
{
    public class GetTasksForDailyListResponseFactory : IGetTasksForDailyListResponseFactory
    {
        public GetTasksForDailyListResponse GenerateResponse(List<Domain.Models.Task> tasks, TimeZoneInfo timezoneInfo)
        {
            var taskDtos = new List<TaskDto>(tasks.Count);

            foreach (var task in tasks)
            {
                var deadlineInUsersTimezone = TimeZoneInfo.ConvertTimeFromUtc(task.Deadline, timezoneInfo);
                taskDtos.Add(new TaskDto(task.Id, task.Title, task.Description, deadlineInUsersTimezone, task.Done, task.DailyListId));
            }

            return new GetTasksForDailyListResponse(taskDtos);
        }
    }
}
