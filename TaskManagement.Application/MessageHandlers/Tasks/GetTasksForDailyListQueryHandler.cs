using FluentValidation;
using MediatR;
using TaskManagement.Api.Controllers;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.Responses.Dtos;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers
{
    public class GetTasksForDailyListQueryHandler : IRequestHandler<GetTasksForDailyListQuery, Result<GetTasksForDailyListResponse>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDailyListRepository _dailyListRepository;
        private readonly IValidator<GetTasksForDailyListQuery> _validator;
        private readonly IGetTasksForDailyListResponseFactory _getTasksForDailyListResponseFactory;

        public GetTasksForDailyListQueryHandler(ITaskRepository taskRepository,
                                                IValidator<GetTasksForDailyListQuery> validator,
                                                IDailyListRepository dailyListRepository,
                                                IUserRepository userRepository, 
                                                IGetTasksForDailyListResponseFactory getTasksForDailyListResponseFactory)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _getTasksForDailyListResponseFactory = getTasksForDailyListResponseFactory ?? throw new ArgumentNullException(nameof(getTasksForDailyListResponseFactory));
        }

        public async Task<Result<GetTasksForDailyListResponse>> Handle(GetTasksForDailyListQuery request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
                return result.CreateErrorResult<GetTasksForDailyListResponse>();

            var listExists = await _dailyListRepository.Exists(request.DailyListId, request.UserId);

            if (!listExists)
                return Result.Error<GetTasksForDailyListResponse>("Daily list does not exist.");

            string userTimezoneId = await _userRepository.GetTimezoneId(request.UserId);
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(userTimezoneId);

            var deadlineTimeLimitUtc = TimeZoneInfo.ConvertTimeToUtc(request.DeadlineLimit!.Value, timezoneInfo);

            var tasks = await _taskRepository.Get(request.DailyListId, request.Done, deadlineTimeLimitUtc);

            var response = _getTasksForDailyListResponseFactory.GenerateResponse(tasks, timezoneInfo);

            return Result.Ok(response);
        }

    }
}
