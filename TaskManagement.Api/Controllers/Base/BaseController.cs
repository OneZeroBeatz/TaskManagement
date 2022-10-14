using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Api.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;
        protected readonly ICurrentUserService _currentUser;

        protected BaseController(IMediator mediator, ICurrentUserService currentUser)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        protected int? GetLoggedUserId() => _currentUser.UserId;
    }
}
