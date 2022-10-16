using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.Tasks;

public class UpdateTaskDoneStatusCommandHandler : IRequestHandler<UpdateTaskDoneStatusCommand, Result>
{
    //TODO: Use generic repository, consider generic factory and handlers by operation
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskFactory _taskFactory;
    private readonly IValidator<UpdateTaskDoneStatusCommand> _validator;

    public UpdateTaskDoneStatusCommandHandler(IValidator<UpdateTaskDoneStatusCommand> validator,
                                              ITaskRepository taskRepository,
                                              ITaskFactory taskFactory)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        _taskFactory = taskFactory ?? throw new ArgumentNullException(nameof(taskFactory));
    }

    public async Task<Result> Handle(UpdateTaskDoneStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result.CreateErrorResult();

        var task = await _taskFactory.CreateTaskAsync(request, cancellationToken);

        await _taskRepository.UpdateAsync(task, cancellationToken);

        return Result.Ok();
    }
}
