using TaskManagement.Api.Controllers;

namespace TaskManagement.Application.Interfaces
{
    public interface IGetTasksForDailyListResponseFactory
    {
        GetTasksForDailyListResponse GenerateResponse(List<Domain.Models.Task> tasks, TimeZoneInfo timezoneInfo);
    }
}