using System.Text;
using FluentValidation;
using GistCopy.Application;
using GistCopy.Application.Services;
using GistCopy.Application.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace GistCopy.WebApi.Extensions;

public static class ServicesExtensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        // Main
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                In = ParameterLocation.Header, 
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey 
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { 
                    new OpenApiSecurityScheme 
                    { 
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer" 
                        } 
                    },
                    Array.Empty<string>()
                } 
            });
        });
        
        // Logging
        var logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .WriteTo.Console()
            .CreateLogger();
        
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog(logger);
        
        // Auth
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
        
        // CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policyBuilder =>
            {
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();
                policyBuilder.AllowAnyOrigin();
            });
        });
        
        // DbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString(nameof(ApplicationDbContext))));
        
        // Validators
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
        
        // Services
        builder.Services.AddScoped<GistService>();
        builder.Services.AddScoped<CommentService>();
        builder.Services.AddScoped<UserService>();
    }
}