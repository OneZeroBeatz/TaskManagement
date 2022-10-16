using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.DataAccess.Repositories.Base;

namespace TaskManagement.Infrastructure.DataAccess.Repositories
{
    public class DailyListRepository : BaseRepository<DailyList>, IDailyListRepository
    {
        public DailyListRepository(TaskManagementDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<DailyList>> GetAsync(int userId,
                                              DateTime date,
                                              string title,
                                              int page,
                                              int pageSize,
                                              CancellationToken token = default)
        {
            var dailyLists = GetBy(userId, date, title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync(token);

            return dailyLists;
        }

        public Task<int> GetCountAsync(int userId, DateTime date, string title, CancellationToken token = default)
        {
            var count = GetBy(userId, date, title).CountAsync(token);

            return count;
        }

        public Task<bool> Exists(int id, int userId, CancellationToken token)
        {
            return DbContext.DailyLists
                .AsNoTracking()
                .Where(x=>x.Id == id)
                .Where(x=>x.UserId == userId)
                .AnyAsync(token);
        }

        private IQueryable<DailyList> GetBy(int userId, DateTime date, string title)
        {
            return DbContext.DailyLists
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Where(x => x.Date == date.Date)
                .Where(x => x.Title.Contains(title)); 
        }
    }
}
