using System.Text;
using FluentValidation;
using GistCopy.Application;
using GistCopy.Application.Services;
using GistCopy.Application.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GistCopy.WebApi.Services;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Main
        services.AddControllers();
        services.AddSwaggerGen(c =>
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
                  new string[] { } 
                } 
              });
        });
        
        // Auth
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
        
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
        services.AddScoped<GistService>();
        services.AddScoped<CommentService>();
        services.AddScoped<UserService>();

        return services;
    }
}