using TaskManagement.Infrastructure.DataAccess.Repositories.Base;

namespace TaskManagement.Application.Repositories
{
    public interface ITaskRepository: IRepository<Domain.Models.Task>
    {
        Task<List<Domain.Models.Task>> GetAsync(int dailyListId, bool done, DateTime? deadlineLimit, CancellationToken token = default);
        Task<int> GetFinishedTasksForDateCountAsync(int userId, DateTime date, CancellationToken token = default);
    }
}