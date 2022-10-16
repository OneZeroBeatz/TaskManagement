using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.Tasks;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<int>>
{
    //TODO: Use generic repository, consider generic factory and handlers by operation
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskFactory _taskFactory;
    private readonly IValidator<CreateTaskCommand> _validator;

    public CreateTaskCommandHandler(IValidator<CreateTaskCommand> validator,
                                    ITaskRepository taskRepository,
                                    ITaskFactory taskFactory)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        _taskFactory = taskFactory ?? throw new ArgumentNullException(nameof(taskFactory));
    }

    public async Task<Result<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result.CreateErrorResult<int>();

        var task = await _taskFactory.CreateTaskAsync(request, cancellationToken);

        task = await _taskRepository.InsertAsync(task, cancellationToken);

        return Result.Ok(task.Id);
    }
}
