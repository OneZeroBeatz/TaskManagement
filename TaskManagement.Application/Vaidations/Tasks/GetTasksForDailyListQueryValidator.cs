using FluentValidation;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Vaidations.Tasks;

public class GetTasksForDailyListQueryValidator : AbstractValidator<GetTasksForDailyListQuery>
{
    private readonly IDailyListRepository _dailyListRepository;
    public GetTasksForDailyListQueryValidator(IDailyListRepository dailyListRepository)
    {
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));

        RuleFor(command => command).NotNull();
        RuleFor(command => command.DeadlineLimit).NotNull();
        RuleFor(command => command)
            .MustAsync(Exists)
            .WithMessage("Daily list does not exist");
    }

    public Task<bool> Exists(GetTasksForDailyListQuery command, CancellationToken token)
        => _dailyListRepository.Exists(command.DailyListId, command.UserId, token);
}
