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
    }
}
