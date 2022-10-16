using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.Tasks;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result>
{
    //TODO: Use generic repository, consider generic handlers by operation
    private readonly ITaskRepository _taskRepository;
    private readonly IValidator<DeleteTaskCommand> _validator;

    public DeleteTaskCommandHandler(IValidator<DeleteTaskCommand> validator,
                                    ITaskRepository taskRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    }

    public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result.CreateErrorResult();

        await _taskRepository.DeleteAsync(request.TaskId, cancellationToken);

        return Result.Ok();
    }
}
