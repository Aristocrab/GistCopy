using GistCopy.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddServices();

var app = builder.Build();
app.UseCustomMiddleware();

app.Run();