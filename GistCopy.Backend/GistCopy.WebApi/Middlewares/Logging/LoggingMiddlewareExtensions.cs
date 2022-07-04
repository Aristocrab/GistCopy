using Microsoft.AspNetCore.Builder;

namespace GistCopy.WebApi.Middlewares.Logging;

public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRoutesCustomLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}