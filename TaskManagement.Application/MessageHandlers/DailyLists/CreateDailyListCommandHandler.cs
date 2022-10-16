using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.DailyLists;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.DailyLists;

public class CreateDailyListCommandHandler : IRequestHandler<CreateDailyListCommand, Result<int>>
{
    //TODO: Use generic repository, consider generic factory and handlers by operation
    private readonly IDailyListRepository _dailyListRepository;
    private readonly IDailyListFactory _dailyListFactory;
    private readonly IValidator<CreateDailyListCommand> _validator;

    public CreateDailyListCommandHandler(IValidator<CreateDailyListCommand> validator,
                                         IDailyListRepository dailyListRepository,
                                         IDailyListFactory dailyListFactory)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
        _dailyListFactory = dailyListFactory ?? throw new ArgumentNullException(nameof(dailyListFactory));
    }

    public async Task<Result<int>> Handle(CreateDailyListCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result.CreateErrorResult<int>();

        var dailyList = _dailyListFactory.CreateDailyList(request);

        dailyList = await _dailyListRepository.InsertAsync(dailyList, cancellationToken);

        return Result.Ok(dailyList.Id);
    }
}
