using TaskManagement.Application.Messages.Responses;
using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Interfaces
{
    public interface IGetDailyListsResponseFactory
    {
        GetDailyListsResponse GenerateResponse(int page, int pageCount, int pageSize, List<DailyList> dailyListsForPage);
    }
}