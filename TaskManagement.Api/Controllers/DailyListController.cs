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

        [HttpGet]//("startDate={startDate}/endDate={endDate}/title={title}/page={page}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> Get(DateTime? startDate, string? title, int page)
        {
            var claimIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimIdentity!.FindFirst(ClaimTypes.Email);

            //TODO: Move request generation to factory classes for each controller
            var query = new GetDailyListsQuery
            {
                Date = startDate,
                Title = string.IsNullOrEmpty(title)? string.Empty : title,
                UserEmail = claim!.Value,
                Page = page,
            };

            var result = await _mediator.Send(query);

            if(result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        } 
    }
}