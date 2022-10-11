using FluentValidation;
using TaskManagement.Application.Messages;

namespace TaskManagement.Application.Vaidations
{
    public class UpdateTimezoneCommandValidator : AbstractValidator<UpdateTimezoneCommand>
    {
        public UpdateTimezoneCommandValidator()
        {
            RuleFor(command => command.TimeZoneId).NotNull();
        }
    }
}
