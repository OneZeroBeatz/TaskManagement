﻿using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.DailyLists;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.DailyLists;

public class UpdateDailyListCommandHandler : IRequestHandler<UpdateDailyListCommand, Result>
{
    private readonly IDailyListRepository _dailyListRepository;
    private readonly IDailyListFactory _dailyListFactory;
    private readonly IValidator<UpdateDailyListCommand> _validator;

    public UpdateDailyListCommandHandler(IValidator<UpdateDailyListCommand> validator,
                                         IDailyListRepository dailyListRepository,
                                         IDailyListFactory dailyListFactory)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
        _dailyListFactory = dailyListFactory ?? throw new ArgumentNullException(nameof(dailyListFactory));
    }

    public async Task<Result> Handle(UpdateDailyListCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result.CreateErrorResult();

        var dailyList = _dailyListFactory.CreateDailyList(request);

        await _dailyListRepository.UpdateAsync(dailyList, cancellationToken);

        return Result.Ok();
    }
}
