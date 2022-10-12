using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages;
using TaskManagement.Application.Messages.Responses;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers
{
    public class CreateDailyListCommandHandler : IRequestHandler<CreateDailyListCommand, Result>
    {
        private readonly IUserRepository _userRepository; 
        private readonly IDailyListRepository _dailyListRepository;
        private readonly IValidator<CreateDailyListCommand> _validator;

        public CreateDailyListCommandHandler(IUserRepository userRepository,
                                             IValidator<CreateDailyListCommand> validator,
                                             IDailyListRepository dailyListRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
        }

        public async Task<Result> Handle(CreateDailyListCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
                return result.CreateErrorResult();

            var user = await _userRepository.GetByEmail(request.UserEmail);
            if (user == null)
                return Result.Error<GetDailyListsResponse>("User does not exist");

            //TODO: Create factory
            var dailyList = new Domain.Models.DailyList()
            {
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                UserId = user.Id
            };

            await _dailyListRepository.Create(dailyList);

            return Result.Ok();
        }
    }
}
