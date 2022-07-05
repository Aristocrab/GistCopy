using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using GistCopy.Application.Dto.User;
using GistCopy.Domain.Entities;
using GistCopy.Domain.Exceptions;
using Mapster;
using Microsoft.IdentityModel.Tokens;

namespace GistCopy.Application.Services;

public class UserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<User> _validator;
    
    public UserService(ApplicationDbContext dbContext, IValidator<User> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    public User RegisterUser(RegisterDto registerDto)
    {
        var user = registerDto.Adapt<User>();

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return user;
    }

    public User GetLoggedUser(LoginDto loginDto)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == loginDto.Username && 
                                                        u.Password == loginDto.Password);
        if (user is null)
        {
            throw new NotFoundException(nameof(User), Guid.Empty);
        }

        return user;
    }

    public string GenerateToken(User user, string jwtKey, string issuer, string audience)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // todo: roles?
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