using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<int>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDailyListRepository _dailyListRepository;
        private readonly IValidator<CreateTaskCommand> _validator;

        public CreateTaskCommandHandler(IValidator<CreateTaskCommand> validator,
                                        IDailyListRepository dailyListRepository, 
                                        IUserRepository userRepository, 
                                        ITaskRepository taskRepository)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        public async Task<Result<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
                return result.CreateErrorResult<int>();

            var listExists = await _dailyListRepository.Exists(request.DailyListId, request.UserId);

            if (!listExists)
                return Result.Error<int>("Daily list does not exist.");

            string userTimezoneId = await _userRepository.GetTimezoneId(request.UserId);
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(userTimezoneId);

            //TODO: Create factory
            var task = new Domain.Models.Task()
            {
                Title = request.Title,
                Description = request.Description,
                Deadline = TimeZoneInfo.ConvertTimeToUtc(request.Deadline, timezoneInfo),
                DailyListId = request.DailyListId,
                Done = false,
            };

            task = await _taskRepository.InsertAsync(task);

            return Result.Ok(task.Id);
        }
    }
}
