using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages.Users;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Events;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.Users;

public class UpdateTimezoneCommandHandler : IRequestHandler<UpdateTimezoneCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdateTimezoneCommand> _validator;
    private readonly IMediator _mediator;
    public UpdateTimezoneCommandHandler(IUserRepository userRepository,
                                        IValidator<UpdateTimezoneCommand> validator,
                                        IMediator mediator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _mediator = mediator;
    }
    public async Task<Result> Handle(UpdateTimezoneCommand request, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(request);
        if (!result.IsValid)
            return result.CreateErrorResult();

        var user = await _userRepository.FindAsync(request.UserId, cancellationToken);

        var timezone = TimeZoneInfo.FindSystemTimeZoneById(request.TimeZoneId);

        user!.TimezoneId = request.TimeZoneId;
        await _userRepository.UpdateAsync(user, cancellationToken);

        await _mediator.Publish(new TimezoneUpdatedEvent { UserEmail = user.Email, Timezone = timezone }, cancellationToken);

        return Result.Ok();
    }
}
