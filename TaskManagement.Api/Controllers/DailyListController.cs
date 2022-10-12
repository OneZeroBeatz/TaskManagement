using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.Application.Messages;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyListController : ControllerBase
    {
        public readonly IMediator _mediator;

        public DailyListController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{page}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> Get(int page)
        {
            var claimIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimIdentity!.FindFirst(ClaimTypes.Email);

            var query = new GetDailyListsQuery
            {
                Page = page,
                UserEmail = claim!.Value
            };

            var result = await _mediator.Send(query);

            if(result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        } 
    }
}