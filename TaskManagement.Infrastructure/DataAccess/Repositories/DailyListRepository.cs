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

        public Task<List<DailyList>> Get(int userId, DateTime date, string title, int page, int pageSize)
        {
            var dailyLists = GetBy(userId, date, title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return dailyLists;
        }

        public Task<int> GetCount(int userId, DateTime date, string title)
        {
            var count = GetBy(userId, date, title).CountAsync();

            return count;
        }

        public async Task<bool> Exists(int id, int userId)
        {
            return await DbContext.DailyLists
                .AsNoTracking()
                .Where(x=>x.Id == id)
                .Where(x=>x.UserId == userId)
                .AnyAsync();
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
