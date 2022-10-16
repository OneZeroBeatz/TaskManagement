using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages.DailyLists;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.DailyLists;

public class DeleteDailyListCommandHandler : IRequestHandler<DeleteDailyListCommand, Result>
{
    //TODO: Use generic repository, consider generic handlers by operation
    private readonly IDailyListRepository _dailyListRepository;
    private readonly IValidator<DeleteDailyListCommand> _validator;

    public DeleteDailyListCommandHandler(IValidator<DeleteDailyListCommand> validator,
                                         IDailyListRepository dailyListRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
    }

    public async Task<Result> Handle(DeleteDailyListCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result.CreateErrorResult();

        await _dailyListRepository.DeleteAsync(request.DailyListId, cancellationToken);

        return Result.Ok();
    }
}
