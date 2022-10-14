using FluentValidation;
using TaskManagement.Application.Messages.DailyLists;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Vaidations.DailyLists;

public class DeleteDailyListCommandValidator : AbstractValidator<DeleteDailyListCommand>
{
    private readonly IDailyListRepository _dailyListRepository;

    public DeleteDailyListCommandValidator(IDailyListRepository dailyListRepository)
    {
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));

        RuleFor(command => command).NotNull();
        RuleFor(command => command)
            .MustAsync(Exists)
            .WithMessage("Daily list does not exist");
    }

    public Task<bool> Exists(DeleteDailyListCommand command, CancellationToken token)
    {
        return _dailyListRepository.Exists(command.DailyListId, command.UserId, token);
    }
}
