using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.Users;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.Users;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<LoginCommand> _validator;
    private readonly IAuthenticationTokenFactory _authenticationTokenFactory;

    public LoginCommandHandler(IUserRepository userRepository,
                               IValidator<LoginCommand> validator,
                               IAuthenticationTokenFactory authenticationTokenFactory)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _authenticationTokenFactory = authenticationTokenFactory ?? throw new ArgumentNullException(nameof(authenticationTokenFactory));
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result.CreateErrorResult<string>();

        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        string token = _authenticationTokenFactory.GenerateToken(user!.Id, request.Email);

        return Result.Ok(token);
    }
}
