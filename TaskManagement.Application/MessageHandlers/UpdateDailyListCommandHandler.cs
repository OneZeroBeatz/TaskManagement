using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers
{
    public class UpdateDailyListCommandHandler : IRequestHandler<UpdateDailyListCommand, Result>
    {
        private readonly IUserRepository _userRepository; 
        private readonly IDailyListRepository _dailyListRepository;
        private readonly IValidator<UpdateDailyListCommand> _validator;

        public UpdateDailyListCommandHandler(IUserRepository userRepository,
                                             IValidator<UpdateDailyListCommand> validator,
                                             IDailyListRepository dailyListRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
        }

        public async Task<Result> Handle(UpdateDailyListCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
                return result.CreateErrorResult();

            var user = await _userRepository.GetByEmail(request.UserEmail);
            if (user == null)
                return Result.Error<int>("User does not exist");

            //TODO: Create factory
            var dailyList = new DailyList()
            {
                Id = request.DailyListId,
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                UserId = user.Id
            };

            await _dailyListRepository.UpdateAsync(dailyList);

            return Result.Ok();
        }
    }
}
