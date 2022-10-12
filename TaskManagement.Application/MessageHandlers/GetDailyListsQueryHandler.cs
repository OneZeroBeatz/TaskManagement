using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages;
using TaskManagement.Application.Messages.Responses;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers
{
    public class GetDailyListsQueryHandler : IRequestHandler<GetDailyListsQuery, Result<GetDailyListsResponse>>
    {
        private readonly IDailyListRepository _dailyListRepository;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<GetDailyListsQuery> _validator;

        //TODO: Move to configuration
        private const int PageSize = 2;

        public GetDailyListsQueryHandler(IDailyListRepository dailyListRepository,
                                         IUserRepository userRepository,
                                         IValidator<GetDailyListsQuery> validator)
        {
            _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<GetDailyListsResponse>> Handle(GetDailyListsQuery request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
                return result.CreateErrorResult<GetDailyListsResponse>();

            var user = await _userRepository.GetByEmail(request.UserEmail);
            if (user == null)
                return Result.Error<GetDailyListsResponse>("User does not exist");

            var totalNumberOfDailyLists = await _dailyListRepository.GetCountForUser(user.Id);

            var pageCount = Math.Ceiling((double)totalNumberOfDailyLists / PageSize);

            if (request.Page > pageCount)
                return Result.Error<GetDailyListsResponse>("There is no such page");

            var dailyListsForPage = await _dailyListRepository.Get(request.Page, PageSize, user.Id);

            var response = new GetDailyListsResponse()
            {
                DailyLists = dailyListsForPage,
                Page = request.Page,
                PageCount = (int)pageCount
            };

            return Result.Ok(response);
        }
    }
}
