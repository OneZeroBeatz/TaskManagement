using FluentValidation;
using TaskManagement.Application.Messages.Tasks;

namespace TaskManagement.Application.Vaidations.Tasks;

public class GetTasksForDailyListQueryValidator : AbstractValidator<GetTasksForDailyListQuery>
{
    public GetTasksForDailyListQueryValidator()
    {
        RuleFor(command => command).NotNull();
        RuleFor(command => command.DeadlineLimit).NotNull();
    }
}
