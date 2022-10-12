using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;

namespace TaskManagement.Infrastructure.DataAccess.Repositories
{
    public class DailyListRepository : IDailyListRepository
    {
        private readonly TaskManagementDbContext _dbContext;

        public DailyListRepository(TaskManagementDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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

        public async Task<int> InsertAsync(DailyList dailyList)
        {
            _dbContext.Add(dailyList);
            await _dbContext.SaveChangesAsync();
            return dailyList.Id;
        }

        public async System.Threading.Tasks.Task UpdateAsync(DailyList dailyList)
        {
            _dbContext.Update(dailyList);

            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<DailyList> GetBy(int userId, DateTime date, string title)
        {
            return _dbContext.DailyLists
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Where(x => x.Date == date.Date)
                .Where(x => x.Title.Contains(title)); 
        }
    }
}
