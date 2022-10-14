using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.DataAccess.Repositories.Base;

namespace TaskManagement.Application.Repositories
{
    public interface IDailyListRepository : IRepository<DailyList>
    {
        Task<int> GetCount(int userId, DateTime date, string title);
        Task<List<DailyList>> Get(int userId, DateTime date, string title, int page, int pageSize);
        Task<bool> Exists(int id, int userId, CancellationToken token);
    }
}