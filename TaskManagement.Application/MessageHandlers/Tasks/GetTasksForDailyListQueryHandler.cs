using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.Responses;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.Tasks;

public class GetTasksForDailyListQueryHandler : IRequestHandler<GetTasksForDailyListQuery, Result<GetTasksForDailyListResponse>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<GetTasksForDailyListQuery> _validator;
    private readonly IGetTasksForDailyListResponseFactory _getTasksForDailyListResponseFactory;

    public GetTasksForDailyListQueryHandler(ITaskRepository taskRepository,
                                            IValidator<GetTasksForDailyListQuery> validator,
                                            IUserRepository userRepository,
                                            IGetTasksForDailyListResponseFactory getTasksForDailyListResponseFactory)
    {
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _getTasksForDailyListResponseFactory = getTasksForDailyListResponseFactory ?? throw new ArgumentNullException(nameof(getTasksForDailyListResponseFactory));
    }

    public async Task<Result<GetTasksForDailyListResponse>> Handle(GetTasksForDailyListQuery request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result.CreateErrorResult<GetTasksForDailyListResponse>();

        string userTimezoneId = await _userRepository.GetTimezoneId(request.UserId, cancellationToken);
        var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(userTimezoneId);

        var deadlineTimeLimitUtc = TimeZoneInfo.ConvertTimeToUtc(request.DeadlineLimit!.Value, timezoneInfo);

        var tasks = await _taskRepository.Get(request.DailyListId, request.Done, deadlineTimeLimitUtc);

        var response = _getTasksForDailyListResponseFactory.GenerateResponse(tasks, timezoneInfo);

        return Result.Ok(response);
    }

}
