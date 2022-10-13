using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers
{
    public class DeleteDailyListCommandHandler : IRequestHandler<DeleteDailyListCommand, Result>
    {
        private readonly IDailyListRepository _dailyListRepository;
        private readonly IValidator<DeleteDailyListCommand> _validator;

        public DeleteDailyListCommandHandler(IValidator<DeleteDailyListCommand> validator,
                                             IDailyListRepository dailyListRepository)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
        }

        public async Task<Result> Handle(DeleteDailyListCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
                return result.CreateErrorResult();

            var dailyListExists = await _dailyListRepository.Exists(request.DailyListId);

            if (!dailyListExists)
                return Result.Error<int>("Daily list does not exist.");

            await _dailyListRepository.DeleteAsync(request.DailyListId);

            return Result.Ok();
        }
    }
}
