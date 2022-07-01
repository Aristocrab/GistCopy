using GistCopy.WebApi;
using GistCopy.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddServices(builder.Configuration);
builder.Host.UseSerilog(logger );

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseCustomExceptionHandler();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();