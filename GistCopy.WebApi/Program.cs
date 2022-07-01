using GistCopy.WebApi;
using GistCopy.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();
app.UseCustomExceptionHandler();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();