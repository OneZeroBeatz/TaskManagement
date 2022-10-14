using TaskManagement.Api.Middlewares;

namespace TaskManagement.Api.Extensions;

public static class UserPersistenceCheckMiddlewareExtensions
{
    public static IApplicationBuilder UseUserPersistenceCheck(this IApplicationBuilder builder)
    {
        return builder.UseWhen(httpContext => !httpContext.Request.Path.StartsWithSegments($"/api/authentication"),
                               subApp => subApp.UseMiddleware<UserPersistenceCheckMiddleware>());
    }
}
