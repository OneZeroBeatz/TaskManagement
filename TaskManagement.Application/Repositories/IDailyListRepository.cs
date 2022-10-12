using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Repositories
{
    public interface IDailyListRepository
    {
        Task<List<DailyList>> Get(int page, int pageSize, int userId);
        Task<int> GetCountForUser(int userId);
    }
}