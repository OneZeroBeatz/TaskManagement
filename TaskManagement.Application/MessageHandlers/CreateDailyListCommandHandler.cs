using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages;
using TaskManagement.Application.Messages.Responses;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers
{
    public class CreateDailyListCommandHandler : IRequestHandler<CreateDailyListCommand, Result<int>>
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

        public async Task<Result<int>> Handle(CreateDailyListCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
                return result.CreateErrorResult<int>();

            var user = await _userRepository.GetByEmail(request.UserEmail);
            if (user == null)
                return Result.Error<int>("User does not exist");

            //TODO: Create factory
            var dailyList = new Domain.Models.DailyList()
            {
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                UserId = user.Id
            };

            var dailyListId = await _dailyListRepository.InsertAsync(dailyList);

            return Result.Ok(dailyListId);
        }
    }
}
