using FluentValidation;
using TaskManagement.Application.Messages.Users;

namespace TaskManagement.Application.Vaidations.Users;

public class UpdateTimezoneCommandValidator : AbstractValidator<UpdateTimezoneCommand>
{
    public UpdateTimezoneCommandValidator()
    {
        RuleFor(command => command);
        RuleFor(command => command.TimeZoneId)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .Must(ValidTimezone)
            .WithMessage("Invalid timezone id");
}

    private bool ValidTimezone(string timezoneId)
    {
        try
        {
            TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
