using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Repositories
{
    public interface IDailyListRepository
    {
        Task<int> GetCount(int userId, DateTime date, string title);
        Task<List<DailyList>> Get(int userId, DateTime date, string title, int page, int pageSize);
        Task<int> InsertAsync(DailyList dailyList);
        System.Threading.Tasks.Task UpdateAsync(DailyList dailyList);
        System.Threading.Tasks.Task DeleteAsync(int id);
        Task<bool> Exists(int id);
    }
}