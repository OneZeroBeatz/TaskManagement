using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages.Users;
using TaskManagement.Application.Repositories;
using TaskManagement.Infrastructure.Services;
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
    //TODO: Add exception handling filter
    public async Task<Result> Handle(UpdateTimezoneCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
                return result.CreateErrorResult();

            var user = await _userRepository.FindAsync(request.UserId);

            //TODO: Move to fluent validator
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(request.TimeZoneId);

            user!.TimeZoneId = request.TimeZoneId;
            await _userRepository.UpdateAsync(user);

            await _mediator.Publish(new TimezoneUpdatedEvent { UserEmail = user.Email, Timezone = timezone });

            return Result.Ok();
        }
        catch (TimeZoneNotFoundException)
        {
            return Result.Error("Invalid timezone");
        }
    }
}
