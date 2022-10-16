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
        public Task<List<Domain.Models.Task>> GetAsync(int dailyListId, bool done, DateTime? deadlineLimit, CancellationToken token = default)
        {
            var tasks = DbContext.Tasks
                .AsNoTracking()
                .Where(x => x.DailyListId == dailyListId)
                .Where(x => x.Deadline < deadlineLimit)
                .Where(x => x.Done == done)
                .ToListAsync(token);

            return tasks;
        }
        public Task<int> GetFinishedTasksForDateCountAsync(int userId, DateTime date, CancellationToken token = default)
        {
            return DbContext.Tasks
                .Where(x => x.Done)
                .Where(x => x.LastDoneUpdate == date)
                .Include(x => x.DailyList)
                .Where(x => x.DailyList.UserId == userId)
                .CountAsync(token);
        }
    }
}
