using System.Net;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Api.Middlewares;

public class UserPersistenceCheckMiddleware
{
    private readonly RequestDelegate _next;

    public UserPersistenceCheckMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, IUserRepository emailRepository, ICurrentUserService currentUser)
    {
        if (!currentUser.UserId.HasValue)
        {
            await _next(httpContext);
            return;
        }

        var user = await emailRepository.FindAsync(currentUser.UserId.Value);

        if (user == null)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            httpContext.Response.ContentType = "text/plain";

            await httpContext.Response.WriteAsync("User does not exist.");
            return;
        }

        await _next(httpContext);
    }
}
