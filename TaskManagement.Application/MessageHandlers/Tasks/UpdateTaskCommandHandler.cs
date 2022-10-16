using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.Tasks;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result>
{
    //TODO: Use generic repository, consider generic factory and handlers by operation
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskFactory _taskFactory;
    private readonly IValidator<UpdateTaskCommand> _validator;

    public UpdateTaskCommandHandler(IValidator<UpdateTaskCommand> validator,
                                    ITaskFactory taskFactory,
                                    ITaskRepository taskRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _taskFactory = taskFactory ?? throw new ArgumentNullException(nameof(taskFactory));
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    }

    //TODO: Consider merging some queries in order to have less request to db in this process, not urgent, queries are not that complex
    public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result.CreateErrorResult();

        var task = await _taskFactory.CreateTaskAsync(request, cancellationToken);

        await _taskRepository.UpdateAsync(task, cancellationToken);

        return Result.Ok();
    }

}
