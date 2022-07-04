using GistCopy.WebApi.Middlewares;
using GistCopy.WebApi.Middlewares.Logging;
using GistCopy.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddServices(builder.Configuration);
builder.AddSerilog();

var app = builder.Build();

// Custom middlewares
app.UseCustomExceptionHandler();
app.UseRoutesCustomLogging();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();