using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.DataAccess.Repositories.Base;

namespace TaskManagement.Application.Repositories
{
    public interface IDailyListRepository : IRepository<DailyList>
    {
        Task<int> GetCountAsync(int userId, DateTime date, string title, CancellationToken token = default);
        Task<List<DailyList>> GetAsync(int userId, DateTime date, string title, int page, int pageSize, CancellationToken token = default);
        Task<bool> Exists(int id, int userId, CancellationToken token = default);
    }
}