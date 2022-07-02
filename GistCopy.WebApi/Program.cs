using GistCopy.WebApi;
using GistCopy.WebApi.Middlewares;
using GistCopy.WebApi.Middlewares.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddServices(builder.Configuration);
builder.Logging.ClearProviders();
builder.Host.UseSerilog(logger);

var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();