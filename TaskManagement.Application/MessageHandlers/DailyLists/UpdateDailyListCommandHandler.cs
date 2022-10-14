using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages.DailyLists;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.DailyLists;

public class UpdateDailyListCommandHandler : IRequestHandler<UpdateDailyListCommand, Result>
{
    private readonly IDailyListRepository _dailyListRepository;
    private readonly IValidator<UpdateDailyListCommand> _validator;

    public UpdateDailyListCommandHandler(IValidator<UpdateDailyListCommand> validator,
                                         IDailyListRepository dailyListRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
    }

    public async Task<Result> Handle(UpdateDailyListCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result.CreateErrorResult();

        //TODO: Create factory
        var dailyList = new DailyList()
        {
            Id = request.DailyListId,
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            UserId = request.UserId
        };

        await _dailyListRepository.UpdateAsync(dailyList, cancellationToken);

        return Result.Ok();
    }
}
