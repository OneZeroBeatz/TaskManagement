using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskManagement.Api.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected BaseController(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected string GetLoggedUserEmail()
        {
            var claimIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimIdentity!.FindFirst(ClaimTypes.Email);
            return claim!.Value;
        }

    }
}
