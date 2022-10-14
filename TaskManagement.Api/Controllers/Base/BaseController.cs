using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Api.Controllers.Base
{
    //TODO: Add base members
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;
        protected readonly ICurrentUserService CurrentUser;

        protected BaseController(IMediator mediator, ICurrentUserService currentUser)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }
    }
}
