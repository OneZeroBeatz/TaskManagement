using FluentValidation;
using TaskManagement.Application.Messages.Users;

namespace TaskManagement.Application.Vaidations
{
    public class UpdateTimezoneCommandValidator : AbstractValidator<UpdateTimezoneCommand>
    {
        public UpdateTimezoneCommandValidator()
        {
            RuleFor(command => command.TimeZoneId).NotNull().NotEmpty().MaximumLength(50);
        }
    }
}
