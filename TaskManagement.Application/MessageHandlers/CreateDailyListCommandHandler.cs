using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers
{
    public class CreateDailyListCommandHandler : IRequestHandler<CreateDailyListCommand, Result<int>>
    {
        private readonly IDailyListRepository _dailyListRepository;
        private readonly IValidator<CreateDailyListCommand> _validator;

        public CreateDailyListCommandHandler(IValidator<CreateDailyListCommand> validator,
                                             IDailyListRepository dailyListRepository)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
        }

        public async Task<Result<int>> Handle(CreateDailyListCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
                return result.CreateErrorResult<int>();

            //TODO: Create factory
            var dailyList = new DailyList()
            {
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                UserId = request.UserId
            };

            dailyList = await _dailyListRepository.InsertAsync(dailyList);

            return Result.Ok(dailyList.Id);
        }
    }
}
