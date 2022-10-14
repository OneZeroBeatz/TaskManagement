using TaskManagement.Application.Messages.Responses;

namespace TaskManagement.Application.Interfaces
{
    public interface IGetTasksForDailyListResponseFactory
    {
        GetTasksForDailyListResponse GenerateResponse(List<Domain.Models.Task> tasks, TimeZoneInfo timezoneInfo);
    }
}