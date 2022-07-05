using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GistCopy.Application.Dto.User;
using GistCopy.Domain.Entities;
using GistCopy.Domain.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GistCopy.Application.Services;

public class UserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<LoginDto> _loginDtoValidator;
    private readonly IValidator<RegisterDto> _registerDtoValidator;

    public UserService(ApplicationDbContext dbContext, 
        IValidator<LoginDto> loginDtoValidator, IValidator<RegisterDto> registerDtoValidator)
    {
        _dbContext = dbContext;
        _loginDtoValidator = loginDtoValidator;
        _registerDtoValidator = registerDtoValidator;
    }

    public async Task<User> RegisterUser(RegisterDto registerDto)
    {
        var result = await _registerDtoValidator.ValidateAsync(registerDto);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        
        var user = registerDto.Adapt<User>();

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User> LoginUser(LoginDto loginDto)
    {
        var result = await _loginDtoValidator.ValidateAsync(loginDto);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username && 
                                                        u.Password == loginDto.Password);
        if (user is null)
        {
            throw new NotFoundException(nameof(User), Guid.Empty);
        }

        return user;
    }

    public static string GenerateToken(User user, string jwtKey, string issuer, string audience)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {   
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim("Id", user.Id.ToString())
        };

        var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}