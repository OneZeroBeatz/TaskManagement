using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Repositories;
using TaskManagement.Infrastructure.DataAccess.Repositories.Base;

namespace TaskManagement.Infrastructure.DataAccess.Repositories
{
    public class TaskRepository : BaseRepository<Domain.Models.Task>, ITaskRepository
    {
        public TaskRepository(TaskManagementDbContext dbContext) : base(dbContext)
        {
        }

        //TODO: Remove nullable from datetime
        public Task<List<Domain.Models.Task>> Get(int dailyListId, bool done, DateTime? deadlineLimit)
        {
            var tasks = DbContext.Tasks
                .AsNoTracking()
                .Where(x => x.DailyListId == dailyListId)
                .Where(x => x.Deadline < deadlineLimit)
                .Where(x => x.Done == done)
                .ToListAsync();

            return tasks;
        }
        public Task<int> GetFinishedTasksForDateCountAsync(int userId, DateTime date)
        {
            return DbContext.Tasks
                .Where(x => x.Done)
                .Where(x => x.LastDoneUpdate == date)
                .Include(x => x.DailyList)
                .Where(x => x.DailyList.UserId == userId)
                .CountAsync();
        }
    }
}
