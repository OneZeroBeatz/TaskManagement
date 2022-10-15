using TaskManagement.Application.Messages.Tasks;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskFactory
    {
        Task<Domain.Models.Task> CreateTaskAsync(CreateTaskCommand request, CancellationToken cancellationToken);
        Task<Domain.Models.Task> CreateTaskAsync(UpdateTaskCommand request, CancellationToken cancellationToken);
        Task<Domain.Models.Task> CreateTaskAsync(UpdateTaskDoneStatusCommand request, CancellationToken cancellationToken);
    }
}