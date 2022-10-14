using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.Tasks;

public class UpdateTaskDoneStatusCommandHandler : IRequestHandler<UpdateTaskDoneStatusCommand, Result>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IValidator<UpdateTaskDoneStatusCommand> _validator;

    public UpdateTaskDoneStatusCommandHandler(IValidator<UpdateTaskDoneStatusCommand> validator,
                                              ITaskRepository taskRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    }

    public async Task<Result> Handle(UpdateTaskDoneStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result.CreateErrorResult();

        var task = await _taskRepository.FindAsync(request.TaskId, cancellationToken);

        task!.Done = request.Done;
        task.LastDoneUpdate = DateTime.UtcNow.Date;

        await _taskRepository.UpdateAsync(task);

        return Result.Ok();
    }
}
