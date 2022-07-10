using GistCopy.WebApi.Middlewares.CustomExceptionHandler;
using GistCopy.WebApi.Middlewares.Logging;

namespace GistCopy.WebApi.Extensions;

public static class MiddlewareExtensions
{
    public static void UseCustomMiddleware(this WebApplication app)
    {
        // CORS
        app.UseCors("AllowAll");

        // Custom middlewares
        app.UseCustomExceptionHandler();
        app.UseRoutesCustomLogging();

        // Auth
        app.UseAuthentication();
        app.UseAuthorization();

        // Api controllers
        app.MapControllers();

        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}