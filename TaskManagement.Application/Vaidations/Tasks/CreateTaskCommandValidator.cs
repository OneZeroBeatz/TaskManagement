using FluentValidation;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Vaidations.Tasks;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{

    private readonly IDailyListRepository _dailyListRepository;
    public CreateTaskCommandValidator(IDailyListRepository dailyListRepository)
    {
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));

        RuleFor(command => command).NotNull();
        RuleFor(command => command.Deadline).NotNull();
        RuleFor(command => command.Title).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(command => command.Description).NotNull().NotEmpty().MaximumLength(500);
        RuleFor(command => command)
            .MustAsync(DailyListExists)
            .WithMessage("Daily list does not exist");
    }

    public Task<bool> DailyListExists(CreateTaskCommand command, CancellationToken token) 
        => _dailyListRepository.Exists(command.DailyListId, command.UserId, token);
}
