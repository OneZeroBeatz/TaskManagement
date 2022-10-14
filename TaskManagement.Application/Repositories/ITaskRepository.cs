using TaskManagement.Infrastructure.DataAccess.Repositories.Base;

namespace TaskManagement.Application.Repositories
{
    public interface ITaskRepository: IRepository<Domain.Models.Task>
    {
        Task<List<Domain.Models.Task>> Get(int dailyListId, bool done, DateTime? deadlineLimit);
        Task<int> GetFinishedTasksForDateCountAsync(int userId, DateTime date);
    }
}