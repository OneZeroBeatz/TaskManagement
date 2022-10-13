namespace TaskManagement.Application.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Domain.Models.Task>> Get(int dailyListId, bool done, DateTime? deadlineLimit);
        Task<int> InsertAsync(Domain.Models.Task task);
        Task<Domain.Models.Task?> Find(int taskId);
        Task UpdateAsync(Domain.Models.Task task);
        Task DeleteAsync(int id);
    }
}