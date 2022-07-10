using FluentValidation;
using GistCopy.Application.Dto.User;
using GistCopy.Application.Services;
using GistCopy.Application.Tests.Common;
using GistCopy.Application.Validators;
using GistCopy.Domain.Exceptions;
using Xunit;

namespace GistCopy.Application.Tests;

public class UserServiceTests
{
    private readonly UserService _userService;
    
    public UserServiceTests()
    {
        var dbContext = TestHelper.CreateDbContext();
        var loginValidator = new LoginDtoValidator();
        var registerValidator = new RegisterDtoValidator();
        _userService = new UserService(dbContext, loginValidator, registerValidator);
    }

    [Fact]
    public void Cant_register_user_with_invalid_credentials()
    {
        var registerDto = new RegisterDto()
        {
            Username = "",
            Password = ""
        };

        Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await _userService.RegisterUser(registerDto);
        });
    }
    
    [Fact]
    public void Cant_login_user_with_invalid_credentials()
    {
        var loginDto = new LoginDto()
        {
            Username = "",
            Password = ""
        };

        Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await _userService.LoginUser(loginDto);
        });
    }
    
    [Fact]
    public void Cant_login_nonexisting_user()
    {
        var loginDto = new LoginDto
        {
            Username = "Username",
            Password = "Password"
        };

        Assert.ThrowsAsync<UserNotFoundException>(async () =>
        {
            await _userService.LoginUser(loginDto);
        });
    }
}