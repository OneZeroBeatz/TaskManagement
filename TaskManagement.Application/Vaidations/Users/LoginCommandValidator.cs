using FluentValidation;
using TaskManagement.Application.Messages.Users;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Vaidations.Users;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public readonly IUserRepository _userRepository;
    public LoginCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        RuleFor(command => command);
        RuleFor(command => command.Password)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(command => command.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(50);

        RuleFor(command => command)
            .MustAsync(HaveValidCredentials)
            .WithMessage("Invalid credentials.");
    }

    private async Task<bool> HaveValidCredentials(LoginCommand command, CancellationToken token)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email, token);

        if (user == null)
            return false;

        if (user.Password != command.Password)
            return false;

        return true;
    }
}
