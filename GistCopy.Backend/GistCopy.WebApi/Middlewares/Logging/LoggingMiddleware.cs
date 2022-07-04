namespace GistCopy.WebApi.Middlewares.Logging;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public Task Invoke(HttpContext context)
    {
        _logger.LogInformation("Request: [{Method}] {Name}", context.Request.Method, context.Request.Path);

        return _next.Invoke(context);
    }
}