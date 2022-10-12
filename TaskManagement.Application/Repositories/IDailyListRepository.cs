using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Repositories
{
    public interface IDailyListRepository
    {
        System.Threading.Tasks.Task Create(DailyList dailyList);
        Task<List<DailyList>> Get(int userId, DateTime date, string title, int page, int pageSize);
        Task<int> GetCount(int userId, DateTime date, string title);
    }
}