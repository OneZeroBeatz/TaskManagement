using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers
{
    public class UpdateTimezoneCommandHandler : IRequestHandler<UpdateTimezoneCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UpdateTimezoneCommand> _validator;

        public UpdateTimezoneCommandHandler(IUserRepository userRepository,
                                            IValidator<UpdateTimezoneCommand> validator)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result> Handle(UpdateTimezoneCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _validator.Validate(request);
                if (!result.IsValid)
                    return result.CreateErrorResult();

                var timezone = TimeZoneInfo.FindSystemTimeZoneById(request.TimeZoneId);

                await _userRepository.UpdateTimezone(request.TimeZoneId, request.UserEmail);

                return Result.Ok();
            }
            catch (TimeZoneNotFoundException)
            {
                return Result.Error("Invalid timezone");
            }
        }
    }
}
