using System.Net;
using System.Security.Claims;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Api
{
    public class UserPersistenceCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public UserPersistenceCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IUserRepository emailRepository)
        {
            var claimIdentity = httpContext.User.Identity as ClaimsIdentity;

            if (claimIdentity == null)
            {
                await _next(httpContext);
                return;
            }

            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                await _next(httpContext);
                return;
            }

            var userId = int.Parse(claim.Value);

            var user = await emailRepository.GetById(userId);

            if (user != null)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = "text/plain";

                await httpContext.Response.WriteAsync("User does not exist.");
                return;
            }

            await _next(httpContext);
        }
    }
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserPersistenceCheck(this IApplicationBuilder builder)
        {
            return builder.UseWhen(httpContext => !httpContext.Request.Path.StartsWithSegments($"/api/authentication"),
                                   subApp => subApp.UseMiddleware<UserPersistenceCheckMiddleware>());
        }
    }
}
