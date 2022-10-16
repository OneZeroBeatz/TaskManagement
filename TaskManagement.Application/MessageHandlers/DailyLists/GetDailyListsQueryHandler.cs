using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.DailyLists;
using TaskManagement.Application.Messages.Responses;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.DailyLists;

public class GetDailyListsQueryHandler : IRequestHandler<GetDailyListsQuery, Result<GetDailyListsResponse>>
{
    private readonly IDailyListRepository _dailyListRepository;
    private readonly IGetDailyListsResponseFactory _getDailyListsResponseFactory;
    private readonly IValidator<GetDailyListsQuery> _validator;

    //TODO: Move to configuration
    private const int PageSize = 10;

    public GetDailyListsQueryHandler(IDailyListRepository dailyListRepository,
                                     IValidator<GetDailyListsQuery> validator,
                                     IGetDailyListsResponseFactory getDailyListsResponseFactory)
    {
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _getDailyListsResponseFactory = getDailyListsResponseFactory ?? throw new ArgumentNullException(nameof(getDailyListsResponseFactory));
    }

    public async Task<Result<GetDailyListsResponse>> Handle(GetDailyListsQuery request, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(request);
        if (!result.IsValid)
            return result.CreateErrorResult<GetDailyListsResponse>();

        var totalNumberOfDailyLists = 
            await _dailyListRepository.GetCountAsync(request.UserId, request.Date!.Value, request.Title!, cancellationToken);

        if (totalNumberOfDailyLists == 0)
            return Result.Error<GetDailyListsResponse>("There are no such lists");

        var pageCount = (int)Math.Ceiling((double)totalNumberOfDailyLists / PageSize);

        if (request.Page > pageCount)
            return Result.Error<GetDailyListsResponse>("There is no such page");

        var dailyListsForPage = 
            await _dailyListRepository.GetAsync(request.UserId, request.Date.Value, request.Title!, request.Page, PageSize, cancellationToken);

        var response = _getDailyListsResponseFactory.GenerateResponse(request.Page, pageCount, PageSize, dailyListsForPage);

        return Result.Ok(response);
    }
}
