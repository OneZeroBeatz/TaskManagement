using FluentValidation;
using TaskManagement.Application.Messages.DailyLists;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Vaidations.DailyLists;

public class UpdateDailyListCommandValidator : AbstractValidator<UpdateDailyListCommand>
{
    private readonly IDailyListRepository _dailyListRepository;

    public UpdateDailyListCommandValidator(IDailyListRepository dailyListRepository)
    {
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));

        RuleFor(command => command).NotNull();
        RuleFor(command => command.Date).NotNull();
        RuleFor(command => command.Title).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(command => command.Description).NotNull().NotEmpty().MaximumLength(500);

        RuleFor(command => command)
            .MustAsync(Exists)
            .WithMessage("Daily list does not exist");
    }

    public Task<bool> Exists(UpdateDailyListCommand command, CancellationToken token)
        => _dailyListRepository.Exists(command.DailyListId, command.UserId, token);
}
