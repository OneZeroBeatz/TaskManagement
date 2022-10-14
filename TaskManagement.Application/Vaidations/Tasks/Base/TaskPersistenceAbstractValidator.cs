using FluentValidation;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Vaidations.Tasks.Base
{
    public class TaskPersistenceAbstractValidator<T> : AbstractValidator<T> 
        where T: class
    {
        private readonly IDailyListRepository _dailyListRepository;
        private readonly ITaskRepository _taskRepository;

        protected TaskPersistenceAbstractValidator(IDailyListRepository dailyListRepository, ITaskRepository taskRepository)
        {
            _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        protected async Task<bool> ExistsForUser(int taskId, int userId, CancellationToken token)
        {
            var task = await _taskRepository.FindAsync(taskId, token);

            if (task == null)
                return false;

            var dailyListExists = await _dailyListRepository.Exists(task.DailyListId, userId, token);

            if (!dailyListExists)
                return false;

            return true;
        }
    }
}
