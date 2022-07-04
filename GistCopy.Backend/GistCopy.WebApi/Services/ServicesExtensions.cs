using FluentValidation;
using GistCopy.Application;
using GistCopy.Application.Services;
using GistCopy.Application.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GistCopy.WebApi.Services;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Main
        services.AddControllers();
        services.AddSwaggerGen();
        
        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policyBuilder =>
            {
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();
                policyBuilder.AllowAnyOrigin();
            });
        });
        
        // DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString(nameof(ApplicationDbContext))));
        
        // Validators
        services.AddValidatorsFromAssemblyContaining<GistValidator>();
        
        // Services
        services.AddScoped<GistsService>();
        services.AddScoped<CommentsService>();

        return services;
    }
}