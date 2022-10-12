using MediatR;
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

        public GetDailyListsQueryHandler(IDailyListRepository dailyListRepository, IUserRepository userRepository)
        {
            _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Result<GetDailyListsResponse>> Handle(GetDailyListsQuery request, CancellationToken cancellationToken)
        {
            if (request.Page <= 0)
                return Result.Error<GetDailyListsResponse>("Page must be a positive number");

            var pageSize = 2;

            var user = await _userRepository.GetByEmail(request.UserEmail);
            if (user == null)
                return Result.Error<GetDailyListsResponse>("User does not exist");

            var totalNumberOfDailyLists = await _dailyListRepository.GetCountForUser(user.Id);

            var pageCount = Math.Ceiling((double)totalNumberOfDailyLists / pageSize);

            if (request.Page > pageCount)
                return Result.Error<GetDailyListsResponse>("There is no such page");

            var dailyListsForPage = await _dailyListRepository.Get(request.Page, pageSize, user.Id);

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
