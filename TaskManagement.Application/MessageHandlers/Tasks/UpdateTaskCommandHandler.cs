using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.Tasks;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdateTaskCommand> _validator;

    public UpdateTaskCommandHandler(IValidator<UpdateTaskCommand> validator,
                                    IUserRepository userRepository,
                                    ITaskRepository taskRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    }

    //TODO: Consider merging some queries in order to have less request to db in this process, not urgent, queries are not that complex
    public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result.CreateErrorResult();

        var task = await _taskRepository.FindAsync(request.TaskId, cancellationToken);

        string userTimezoneId = await _userRepository.GetTimezoneId(request.UserId);
        var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(userTimezoneId);

        task!.Title = request.Title;
        task.Description = request.Description;
        task.Deadline = TimeZoneInfo.ConvertTimeToUtc(request.Deadline, timezoneInfo);

        await _taskRepository.UpdateAsync(task);

        return Result.Ok();
    }
}
