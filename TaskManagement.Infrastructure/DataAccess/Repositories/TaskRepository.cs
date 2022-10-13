using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;

namespace TaskManagement.Infrastructure.DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDbContext _dbContext;

        public TaskRepository(TaskManagementDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        //TODO: Remove nullable from datetime
        public Task<List<Domain.Models.Task>> Get(int dailyListId, bool done, DateTime? deadlineLimit)
        {
            var tasks = _dbContext.Tasks
                .AsNoTracking()
                .Where(x => x.DailyListId == dailyListId)
                .Where(x => x.Deadline < deadlineLimit)
                .Where(x => x.Done == done)
                .ToListAsync();

            return tasks;
        }

        public async Task<int> InsertAsync(Domain.Models.Task task)
        {
            _dbContext.Add(task);
            await _dbContext.SaveChangesAsync();
            return task.Id;
        }

        //public async System.Threading.Tasks.Task UpdateAsync(DailyList dailyList)
        //{
        //    _dbContext.Update(dailyList);
        //
        //    await _dbContext.SaveChangesAsync();
        //}
        //
        //public async System.Threading.Tasks.Task DeleteAsync(int id)
        //{
        //    var entity = await _dbContext.DailyLists.FindAsync(id);
        //    _dbContext.DailyLists.Remove(entity!);
        //    await _dbContext.SaveChangesAsync();
        //}
        //
        //public async Task<bool> Exists(int id, int userId)
        //{
        //    return await _dbContext.DailyLists
        //        .AsNoTracking()
        //        .Where(x=>x.Id == id)
        //        .Where(x=>x.UserId == userId)
        //        .AnyAsync();
        //}

    }
}
