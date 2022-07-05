using GistCopy.Application.Dto.User;
using GistCopy.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GistCopy.WebApi.Controllers;

public class UsersController : BaseController
{
    private readonly UserService _userService;
    private readonly IConfiguration _config;

    public UsersController(UserService userService, IConfiguration config)
    {
        _userService = userService;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await _userService.LoginUser(loginDto);
        var token = UserService.GenerateToken(user, 
            _config["Jwt:Key"], _config["Jwt:Issuer"], _config["Jwt:Audience"]);
        
        return Ok(token);
    }
    
    [HttpGet("currentUser")]
    public IActionResult GetCurrentUser()
    {
        var user = CurrentUser;

        return Ok(user);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var user = await _userService.RegisterUser(registerDto);
        var token = UserService.GenerateToken(user, 
            _config["Jwt:Key"], _config["Jwt:Issuer"], _config["Jwt:Audience"]);
        
        return Ok(token);
    }
}