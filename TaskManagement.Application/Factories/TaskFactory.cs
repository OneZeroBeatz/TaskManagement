using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Factories;

public class TaskFactory : ITaskFactory
{
    private readonly IUserRepository _userRepository;
    private readonly ITaskRepository _taskRepository;

    public TaskFactory(IUserRepository userRepository, ITaskRepository taskRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    }

    //TODO: Consider not using commands as parameter here
    public async Task<Domain.Models.Task> CreateTaskAsync(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        string userTimezoneId = await _userRepository.GetTimezoneId(request.UserId, cancellationToken);
        var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(userTimezoneId);

        return new Domain.Models.Task()
        {
            Title = request.Title,
            Description = request.Description,
            Deadline = TimeZoneInfo.ConvertTimeToUtc(request.Deadline, timezoneInfo),
            DailyListId = request.DailyListId,
            Done = false,
        };
    }

    public async Task<Domain.Models.Task> CreateTaskAsync(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.FindAsync(request.TaskId, cancellationToken);

        string userTimezoneId = await _userRepository.GetTimezoneId(request.UserId, cancellationToken);
        var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(userTimezoneId);

        task!.Title = request.Title;
        task.Description = request.Description;
        task.Deadline = TimeZoneInfo.ConvertTimeToUtc(request.Deadline, timezoneInfo);

        return task;
    }

    public async Task<Domain.Models.Task> CreateTaskAsync(UpdateTaskDoneStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.FindAsync(request.TaskId, cancellationToken);

        task!.Done = request.Done;
        task.LastDoneUpdate = DateTime.UtcNow.Date;
        return task;
    }
}
